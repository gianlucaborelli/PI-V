using Service.Api.Core.Entity;

namespace Service.Api.Service.SystemManager.Models
{
    public class ModuleAccessToken : EntityBase
    {
        public Guid ModuleId { get; set; }
        public string Token { get; private set; }
        public bool IsActive { get; private set; } = true; // Default to active
        public DateTime ExpiresAt { get; set; } = DateTime.UtcNow.AddDays(30); // Default expiration of 30 days

        public ModuleAccessToken(Guid moduleId)
        {
            Token = TokenGenerator();
            ModuleId = moduleId;
            IsActive = true;
        }
            
        public void RegenerateToken()
        {
            Token = TokenGenerator();
            IsActive = true; 
            ExpiresAt = DateTime.UtcNow.AddDays(30); 
        }

        public bool IsValid(string token)
        {
            return IsActive && DateTime.UtcNow <= ExpiresAt 
                            && Token == token 
                            && IsActive;
        }

        public bool IsNotValid(string token)
        {
            return !IsValid(token);
        }

        public void Revoke()
        {
            IsActive = false;
        }

        private string TokenGenerator()
        {
            Random random = new Random();
            int[] numeros = new int[8];

            for (int i = 0; i < numeros.Length; i++)
            {
                numeros[i] = random.Next(0, 10); // números entre 0 e 9
            }

            // Junta tudo em uma única string
            return string.Concat(numeros);
        }
    }
}
