using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Web;

namespace b2cwebapp.Pages;

[AuthorizeForScopes(ScopeKeySection = "DownstreamApi:Scopes")]
public class ApiModel : PageModel
{

    private readonly IDownstreamWebApi _downstreamWebApi;

    public ApiModel(IDownstreamWebApi downstreamWebApi)
    {
        _downstreamWebApi = downstreamWebApi;
    }

    public async Task OnGet()
    {
        var response = await _downstreamWebApi.GetForUserAsync<dynamic>("myApi", "/headers").ConfigureAwait(false);

        ViewData["ApiResult"] = response;
    }
}
