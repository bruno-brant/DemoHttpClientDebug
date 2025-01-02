namespace DemoHttpClientDebug;

public class JsonPlaceholderHttpClient(HttpClient httpClient)
{
	public async Task CreatePost(Post post)
	{
		var response = await httpClient.PostAsJsonAsync("posts", post);

		response.EnsureSuccessStatusCode();
	}

	public async Task<Post> GetPost(int id)
	{
		var response = await httpClient.GetAsync($"posts/{id}");
		if (!response.IsSuccessStatusCode)
		{
			throw new Exception("Failed to get post");
		}

		response.EnsureSuccessStatusCode();

		return await response.Content.ReadFromJsonAsync<Post>()
			?? throw new Exception("Invalid content in response");
	}

	public record Post
	{
		public int Id { get; init; }
		public required string Title { get; init; }
		public required string Body { get; init; }
		public int UserId { get; init; }
	}
}
