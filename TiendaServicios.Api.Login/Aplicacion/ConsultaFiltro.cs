
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;
using TiendaServicios.Api.Login.Persistencia;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.Autor.Modelo;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Collections.Generic;

namespace TiendaServicios.Api.Login.Aplicacion
{
    public class ConsultaFiltro
    {
        public class UsuarioUnico : IRequest<DtaRes>
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public class Manejador : IRequestHandler<UsuarioUnico,DtaRes>
        {
            private readonly ContextoUsuario _contextoUsuario;
            private readonly IMapper _mapper;

            public Manejador(ContextoUsuario contextoUsuario, IMapper mapper)
            {
                this._contextoUsuario = contextoUsuario;
                this._mapper = mapper;
            }
            public async Task<DtaRes> Handle(UsuarioUnico request, CancellationToken cancellationToken)
            {
                var Res = new DtaRes();
                var user = await _contextoUsuario.UsuariosLogin
                    .Where(p => p.Username == request.Username && p.Password == request.Password).FirstOrDefaultAsync();

                if (user == null)
                {
                    throw new Exception("No se encontro el usuario");
                }
                else
                {
                    var Token = "";
                    try
                    {
                        var userdto = _mapper.Map<Usuarios, UsuarioDto>(user);
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var key = Encoding.ASCII.GetBytes("my-secret-key-123");
                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(new Claim[]
                            {
            new Claim(ClaimTypes.Name, request.Username)
                            }),
                            Expires = DateTime.UtcNow.AddMinutes(15),
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                        };
                        var token = tokenHandler.CreateToken(tokenDescriptor);
                        var tokenString = tokenHandler.WriteToken(token);
                         Token = tokenString;
                       
                        Res=new DtaRes
                        {
                            IdUser=user.IdUser,
                            Token=Token

                        };


                    }catch(Exception e)
                    {
                        string messae = e.Message;
                    }
                    return Res;
                   
                }
            }

        }
    }
}
