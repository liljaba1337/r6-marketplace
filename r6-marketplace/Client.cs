using r6_marketplace.Utils;
using r6_marketplace.Endpoints;

namespace r6_marketplace
{
    public class Client
    {
        public ItemInfoEndpoints ItemInfoEndpoints { get; }

        private Data.Local language;
        private Web web = new Web();
        public bool isAuthenticated => web.isAuthenticated;
        public Client(Data.Local language = Data.Local.en, string? token = null)
        {
            this.language = language;
            if(token != null) web.SetToken(token);

            ItemInfoEndpoints = new ItemInfoEndpoints(web);
        }
        public async Task AuthenticateAsync(string login, string password)
        {
            r6_marketplace.Classes.AuthenticationResponse response =
                await Authentication.Authentication.AuthenticateAsync(login, password);
            web.SetToken("Ubi_v1 t=" + response.ticket);
        }
    }
}
