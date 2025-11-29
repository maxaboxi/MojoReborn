using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Mojo.Modules.Blog.Data;
using Mojo.Modules.Core.Features.GetModule;
using Mojo.Shared.Responses;

namespace Mojo.Modules.Blog.Features.CreatePost;

public static partial class CreatePostHandler
{
    public static async Task<CreatePostResponse> Handle(
        CreatePostCommand command,
        BlogDbContext db,
        ModuleResolver moduleResolver,
        CancellationToken cancellationToken)
    {
        //TODO: check if user has rights for the module when auth implemented
        var moduleDto = await moduleResolver.GetModuleByPageId(command.PageId, cancellationToken);
        
        if (moduleDto == null)
        {
            return BaseResponse.NotFound<CreatePostResponse>("Module not found.");
        }
            
        var baseSlug = GenerateSlug(command.Title);
        
        var similarSlugs = await db.BlogPosts
            .Where(b => b.Slug == baseSlug || b.Slug.StartsWith(baseSlug + "-"))
            .Select(b => b.Slug)
            .ToListAsync(cancellationToken);

        var finalSlug = baseSlug;

        if (similarSlugs.Count > 0)
        {
            var suffixRegex = new Regex($@"^{Regex.Escape(baseSlug)}-(\d+)$");
            
            var maxSuffix = similarSlugs
                .Select(s => suffixRegex.Match(s))
                .Where(m => m.Success)
                .Select(m => int.Parse(m.Groups[1].Value))
                .DefaultIfEmpty(0)
                .Max();

            finalSlug = $"{baseSlug}-{maxSuffix + 1}";
        }
        
        var newPost = new BlogPost
        {
            Title = command.Title,
            SubTitle = command.SubTitle,
            Author = command.Author,
            Content = command.Content,
            Slug = finalSlug,
            CreatedAt = DateTime.UtcNow,
            ModifiedAt = DateTime.UtcNow,
            ModuleGuid =  moduleDto.ModuleGuid,
            ModuleId = moduleDto.Id
        };
        
        var incomingIds = command.Categories.Where(c => c.Id > 0).Select(c => c.Id).ToList();
        var incomingNames = command.Categories.Select(c => c.CategoryName).Distinct().ToList();
                
        var existingCategoriesInDb = await db.Categories
            .Where(c => incomingIds.Contains(c.Id) || incomingNames.Contains(c.CategoryName))
            .ToListAsync(cancellationToken);

        foreach (var dto in command.Categories)
        {
            Category? match = null;

            if (dto.Id > 0)
            {
                match = existingCategoriesInDb.FirstOrDefault(c => c.Id == dto.Id);
            }

            match ??= existingCategoriesInDb.FirstOrDefault(c =>
                string.Equals(c.CategoryName, dto.CategoryName, StringComparison.CurrentCultureIgnoreCase));

            newPost.Categories.Add(match ?? new Category { CategoryName = dto.CategoryName });
        }
        
        await db.BlogPosts.AddAsync(newPost, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);
        
        return new CreatePostResponse { IsSuccess = true, BlogPostId = newPost.BlogPostId, Message = "Blog post created successfully." };
    }

    private static string GenerateSlug(string title)
    {
        // Max Column Length: 255
        // Date Prefix: "yyyy-MM-dd-" = 11 chars
        // Reserve for Suffix: "-999" = 4 chars
        // Max Title Part: 255 - 11 - 4 = 240
        const int maxTitlePartLength = 240;

        var str = title.ToLowerInvariant();
        str = RemoveInvalidChars().Replace(str, "");
        str = ReplaceMultipleSpacesAndHyphens().Replace(str, " ").Trim();
        str = ReplaceSpaces().Replace(str, "-");

        if (str.Length > maxTitlePartLength)
        {
            str = str.Substring(0, maxTitlePartLength);
            str = str.TrimEnd('-');
        }
        
        var datePrefix = DateTime.UtcNow.ToString("yyyy-MM-dd");

        return $"{datePrefix}-{str}";
    }

    [GeneratedRegex(@"[^a-z0-9\s-]")]
    private static partial Regex RemoveInvalidChars();
    [GeneratedRegex(@"[\s-]+")]
    private static partial Regex ReplaceMultipleSpacesAndHyphens();
    [GeneratedRegex(@"\s")]
    private static partial Regex ReplaceSpaces();
}