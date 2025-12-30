export type BlogComment = {
  id: string;
  author: string;
  userGuid?: string | null;
  title?: string;
  content: string;
  createdAt: string;
};

export type RawBlogCommentDto = {
  id: string;
  userGuid?: string | null;
  userName: string;
  title?: string;
  content: string;
  createdAt: string;
  modifiedAt?: string;
  moderatedBy?: string;
  moderationReason?: string | null;
};

export type Category = {
  id: number;
  categoryName: string;
  moduleId?: number;
};

export type BlogPostSummary = {
  id: number;
  blogPostGuid: string;
  title: string;
  content: string;
  author: string;
  createdAt: string;
  categories: string[];
  commentCount: number;
};

export type BlogPostDetail = BlogPostSummary & {
  subTitle: string;
  comments: BlogComment[];
};

export type CreatePostCategoryDto = {
  id: number;
  categoryName: string;
};

export type CreatePostRequest = {
  pageId: number;
  title: string;
  subTitle: string;
  content: string;
  categories: CreatePostCategoryDto[];
};

export type CreatePostResponse = {
  blogPostId: string;
};

export type EditPostRequest = {
  pageId: number;
  blogPostId: string;
  title: string;
  subTitle: string;
  content: string;
  categories: Category[];
};

export type DeletePostRequest = {
  pageId: number;
  blogPostId: string;
};

export type EditPostResponse = void;

export type GetPostsResponse = {
  blogPosts: BlogPostSummary[];
};

export type BlogPostDetailDto = Omit<BlogPostDetail, 'comments'> & {
  comments: RawBlogCommentDto[];
};

export type CategoryDto = {
  id: number;
  moduleId: number;
  categoryName: string;
};

export type CategoryMutationResponse = void;

export type CreateCategoryRequest = {
  pageId: number;
  categoryName: string;
};

export type EditCategoryRequest = {
  pageId: number;
  categoryId: number;
  categoryName: string;
};

export type DeleteCategoryRequest = {
  pageId: number;
  categoryId: number;
};

export type DeletePostResponse = void;

export type CreateCommentRequest = {
  pageId: number;
  blogPostId: string;
  author: string;
  title: string;
  content: string;
};

export type CreateCommentResponse = {
  commentId: string;
};

export type EditCommentRequest = {
  pageId: number;
  blogPostId: string;
  blogCommentId: string;
  title: string;
  content: string;
};

export type EditCommentResponse = void;

export type DeleteCommentRequest = {
  pageId: number;
  blogPostId: string;
  blogCommentId: string;
};

export type DeleteCommentResponse = void;
