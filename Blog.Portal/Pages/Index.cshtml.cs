using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Portal.Pages;

public class IndexModel : PageModel
{
    #region Fields :
    private readonly IMediator _mediator;
    #endregion

    #region CTORS :
    public IndexModel(IMediator mediator)
    {
        _mediator = mediator;
    }
    #endregion

    #region Actions :
    public void OnGet()
    {

    }
    #endregion
}