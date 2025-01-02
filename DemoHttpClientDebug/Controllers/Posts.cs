using Microsoft.AspNetCore.Mvc;

namespace DemoHttpClientDebug.Controllers;

[ApiController]
[Route("[controller]")]
public class Posts(JsonPlaceholderHttpClient client) : ControllerBase
{
	[HttpPost]
	public async void CreatePost()
	{
		await client.CreatePost(new()
		{
			Id = 1321321654,
			UserId = 121611,
			Body = "Hello World",
			Title = "Hello",
		});
	}
}
