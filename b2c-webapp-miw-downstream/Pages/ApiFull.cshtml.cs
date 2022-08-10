using System.Net;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Web;

namespace b2cwebapp.Pages;

[AuthorizeForScopes(ScopeKeySection = "DownstreamApi:Scopes")]
public class ApiFullModel : PageModel
{
    private readonly IDownstreamWebApi _downstreamWebApi;

    public ApiFullModel(IDownstreamWebApi downstreamWebApi)
    {
        _downstreamWebApi = downstreamWebApi;
    }

    public async Task OnGet()
    {
        
        using var response = await _downstreamWebApi.CallWebApiForUserAsync("myApi", options =>
        {
            options.HttpMethod = HttpMethod.Get;
            options.RelativePath = "/headers";

        }).ConfigureAwait(false);
        

        if (response.StatusCode is HttpStatusCode.OK)
        {
            var apiResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            ViewData["ApiResult"] = apiResult;
        }
        else
        {
            var error = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            throw new HttpRequestException($"Invalid status code in the HttpResponseMessage: {response.StatusCode}: {error}");
        }
    }
}
