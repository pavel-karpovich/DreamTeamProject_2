using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unosquare.Labs.EmbedIO;
using Unosquare.Labs.EmbedIO.Constants;
using Unosquare.Labs.EmbedIO.Modules;

namespace DreamTeamProject
{
    public class AccountController : WebApiController
    {
        private IHttpContext _context;

        public AccountController(IHttpContext context) : base(context)
        {
            _context = context;
        }

        [WebApiHandler(HttpVerbs.Get, "/api/users/{id}")]
        public async Task<bool> GetUserById(int id)
        {
            try
            {
                if (People.Users.Any(p => p.Id == id))
                {
                    return await this.JsonResponseAsync(People.Users.FirstOrDefault(p => p.Id == id));
                }
            }
            catch (Exception ex)
            {
                return await this.JsonExceptionResponseAsync(ex);
            }
            return false;
        }

        [WebApiHandler(HttpVerbs.Get, "/api/users")]
        public async Task<bool> GetUsers()
        {
            try
            {
                return await this.JsonResponseAsync(People.Users);
            }
            catch (Exception ex)
            {
                return await this.JsonExceptionResponseAsync(ex);
            }
        }

        // You can override the default headers and add custom headers to each API Response.
        public override void SetDefaultHeaders() => this.NoCache();

        [WebApiHandler(HttpVerbs.Post, "/api/users")]
        public async Task<bool> PostUser()
        {
            try
            {
                User user = await this.ParseJsonAsync<User>();
                People.Users.Add(user);
                People.Serialize();
                return true;

            }
            catch (Exception ex)
            {
                return await this.JsonExceptionResponseAsync(ex);
            }
        }

        [WebApiHandler(HttpVerbs.Delete, "/api/user/{id}")]
        public void RemoveUser(int? id)
        {
            if (id != null)
            {
                User user = People.Users.Where(u => u.Id == id).FirstOrDefault();
                if (user != null)
                {
                    People.Users.Remove(user);
                    People.Serialize();
                }
            }
        }
    }
}
