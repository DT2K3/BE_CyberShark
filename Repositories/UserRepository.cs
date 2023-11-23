using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BE_cybershark.Models;
using BE_cybershark.Models.BE_cybershark.Models;
using BE_CyberShark.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BE_CyberShark.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CyberSharkContext _context;
        private readonly IConfiguration _config;

        public UserRepository(CyberSharkContext context, IConfiguration config)
        {
            _context = context;
            //inject config into Repo
            _config = config;
        }

        public async Task<User?> GetById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User?> GetByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetByUsername(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Ten == username);
        }

        public async Task<User?> Create(RegisterViewModel registerViewModel)
        {
            var newUser = new User
            {
                Ten = registerViewModel.Ten,
                Mat_khau = registerViewModel.Mat_khau,
                Email = registerViewModel.Email,
                So_dien_thoai = registerViewModel.So_dien_thoai,
                Hinhanh = registerViewModel.Hinhanh,
                Role = registerViewModel.Role,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return newUser;
        }

        public async Task<string> Login(LoginViewModel loginViewModel)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginViewModel.Email);

            if (user != null && VerifyPassword(user.Mat_khau, loginViewModel.Mat_khau))
            {
                // Đăng nhập thành công, tạo JWT token
                var token = GenerateJwtToken(user);
                return token;
            }

            // Đăng nhập thất bại
            throw new Exception("Wrong email or password");
        }

        public string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:SecretKey"] ?? ""));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
            // Thêm các thông tin khác nếu cần
        };

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(30), // Thời hạn của token là 30 ngày
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(token);
        }
        private string HashPassword(string password)
        {
            // In a real system, you should use a secure password hashing library like BCrypt or Identity
            // Here is a simple example using SHA256 for demonstration purposes (not secure for production)
            using (var sha256 = new System.Security.Cryptography.SHA256Managed())
            {
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }
        private bool VerifyPassword(string hashedPassword, string enteredPassword)
        {
            return hashedPassword == HashPassword(enteredPassword);
        }
    }


}
