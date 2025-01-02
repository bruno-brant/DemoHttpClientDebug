
namespace DemoHttpClientDebug;

// This is a sample to show how to do it manually, but one should use the LoggingHttpMessageHandler from Microsoft.Extensions.Http.Logging
public class MyLoggingMessageHandler(ILogger<MyLoggingMessageHandler> logger) : DelegatingHandler
{
	protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		var content = request.Content != null
			? await request.Content.ReadAsStringAsync(cancellationToken)
			: "";

		logger.LogInformation("Sending request => {method} {url}: {content}",request.Method ,request.RequestUri, content);

		return await base.SendAsync(request, cancellationToken);
	}
}
