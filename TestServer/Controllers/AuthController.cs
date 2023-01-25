using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Google.Apis.Auth;
using Google;
using Microsoft.AspNetCore.Authorization;

namespace TestServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private MyContext context;
        public AuthController(IConfiguration configuration, MyContext ctxt)
        {
            _configuration = configuration;
            context = ctxt;
        }
        [HttpGet("Fraud")]
        public IActionResult GetFraudToken()
        {
            string Token = CreateToken(new InstructorDTO("IITJMU000", "Shrey", "sshrey183@gmail.com"));
            return Ok(Token);
        }

        [HttpGet("login")]
        public async Task<IActionResult> GetLogin(string acctoken)
        {
            string Email;
            Console.WriteLine(acctoken);
            try
            {
                GoogleJsonWebSignature.Payload pld = await GoogleJsonWebSignature.ValidateAsync(acctoken);
                Email = pld.Email;
            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid Token");
                return Unauthorized("Invalid id Token");
            }
            string Id, Name;
            Instructor ist = context.Instructors.Where(s => s.email_id == Email).Single();
            Id = ist.instructor_id;
            Name = ist.name;

            string token = CreateToken(new InstructorDTO(Id, Name, Email));
            Console.WriteLine(token);
            return Ok(token);
        }
        private string CreateToken(InstructorDTO instructor)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, instructor.Id),
                new Claim(ClaimTypes.Name, instructor.Name),
                new Claim(ClaimTypes.Email, instructor.Email)
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
        [Authorize]
        [HttpGet("details")]
        public IActionResult UserDetails()
        {
            string? Id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            string? Name = User.FindFirst(ClaimTypes.Name)?.Value;
            string? email = User.FindFirst(ClaimTypes.Email)?.Value;

            if (Id == null || Name == null || email == null)
            {
                return NotFound();
            }
            return Ok(new {instrucor_id = Id, name = Name, email = email});
        }
    }
}
