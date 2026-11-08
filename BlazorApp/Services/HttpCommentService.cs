using ApiContracts;

namespace BlazorApp.Services;

public class HttpCommentService : ICommentService
{
    private readonly HttpClient client;
    
    public HttpCommentService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<CommentDto> AddCommentAsync(CreateCommentDto request)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("comments/", request);
        
        response.EnsureSuccessStatusCode();
        
        CommentDto? comment =  await response.Content.ReadFromJsonAsync<CommentDto>();
        
        return comment ?? throw new InvalidOperationException("Empty response");
    }

    public async Task UpdateCommentAsync(int id, UpdateCommentDto request)
    {
        HttpResponseMessage response = await client.PutAsJsonAsync("comments/" + id, request);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteCommentAsync(int id)
    {
        HttpResponseMessage response = await client.DeleteAsync("comments/" + id);
        
        response.EnsureSuccessStatusCode();
    }
}