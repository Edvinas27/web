using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Supabase;

namespace backend.Services
{
    public class SupabaseService
    {

        private readonly Client _client;

        public SupabaseService(IConfiguration configuration)
        {
            var url = configuration["SupaBase:Url"];
            var key = configuration["SupaBase:Key"];
            if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Supabase URL and Key must be provided in configuration.");
            }

            _client = new Client(url, key, new SupabaseOptions
            {
                AutoRefreshToken = true,
                AutoConnectRealtime = true,
            });

            _client.InitializeAsync().GetAwaiter().GetResult();
        }

        public async Task InitializeAsync()
        {
            try
            {
                await _client.InitializeAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to initialize Supabase client.", ex);
            }
        }

        public Client GetClient() => _client;
    }
}