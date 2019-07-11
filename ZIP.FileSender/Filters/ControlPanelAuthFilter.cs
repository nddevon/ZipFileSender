using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ZIP.FileSender.Filters {
    public class ControlPanelAuthAttribute: TypeFilterAttribute {
        public ControlPanelAuthAttribute() : base(typeof(ControlPanelAuthFilter)) {
        }
    }

    public class ControlPanelAuthFilter: IAuthorizationFilter {

        public void OnAuthorization(AuthorizationFilterContext context) {
            if (!true) {
                context.Result = new ForbidResult();
            }
        }
    }

}
