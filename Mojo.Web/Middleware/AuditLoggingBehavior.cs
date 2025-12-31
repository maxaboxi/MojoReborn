using System.Diagnostics;
using Wolverine;

namespace Mojo.Web.Middleware;

public class AuditLoggingBehavior
{
    public Stopwatch Before(Envelope envelope, ILogger<AuditLoggingBehavior> logger, IHttpContextAccessor httpContextAccessor)
    {
        var user = httpContextAccessor.HttpContext?.User;
        var messageType = envelope.Message?.GetType().Name ?? "Unknown";
        var userName = user?.Identity?.Name ?? "Anonymous";
        
        logger.LogInformation("Executing {MessageType} for user {User}", messageType, userName);
        
        return Stopwatch.StartNew();
    }

    public void Finally(Stopwatch sw, Envelope envelope, ILogger<AuditLoggingBehavior> logger)
    {
        sw.Stop();
        
        var messageType = envelope.Message?.GetType().Name ?? "Unknown";
        
        logger.LogInformation("Finished {MessageType} in {ElapsedMilliseconds}ms", 
            messageType, 
            sw.ElapsedMilliseconds);
    }
}