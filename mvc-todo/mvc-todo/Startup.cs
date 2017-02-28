using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(mvc_todo.Startup))]
namespace mvc_todo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
