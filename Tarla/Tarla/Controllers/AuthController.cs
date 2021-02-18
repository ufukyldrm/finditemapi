using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using System.Security.Cryptography;

using System.Text;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Data;

using Tarla.Models;

using System.Data.Common;
using MySqlConnector;
using System.Text;


using System.Security.Cryptography;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tarla.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {





        [HttpGet]
        [Route("persons")]
        public async Task<IActionResult> GetAllPersons()
        {

            Repos.AuthReposotory query = new Repos.AuthReposotory();
            var result = await query.FindAllPersons();

            if (result is null)
                return new NotFoundResult();
            else
            {

                return Ok(result);

            }



        }




        [HttpGet]
        [Route("yetkiler")]
        public async Task<IActionResult> GetAllYetkis()
        {

            Repos.AuthReposotory query = new Repos.AuthReposotory();
            var result = await query.FindAllYetki();

            if (result is null)
                return new NotFoundResult();
            else
            {

                return Ok(result);

            }

        }

        [HttpPost]
        [Route("updateyetki")]
        public async Task<IActionResult> UpdateYetki([FromBody] DTO.YetkiDto yetki)  //from body şart değil
        {
    
            int id = -1;
            Repos.AuthReposotory query = new Repos.AuthReposotory();

            await query.UpdateYetki(yetki);



            return Ok(id);

        }

        [HttpPost]
        [Route("addperson")]
        public async Task<IActionResult> InsertPerson([FromBody] Models.kullanicilar kullanici)  //from body şart değil
        {



            int id = -1;
            Repos.AuthReposotory query = new Repos.AuthReposotory();

            await query.InsertUser(kullanici);



            return Ok(id);


        }



        [HttpPost]

        [Route("login")]

        public async Task<IActionResult> Login([FromBody] DTO.User user)  //from body şart değil
        {
            Repos.AuthReposotory query = new Repos.AuthReposotory();

            kullanicilar values = await query.Login(user.ad, user.sifre);




            if (values == null)
            {


                var aciklama = "-1";



                return Ok(aciklama);


            }
            else
            {
                var aciklama =await query.NameIdBul(user.ad);


                ////hazır kod

                return Ok(new ObjectResult(query.GenerateToken(user.ad, query.NameIdBul(user.ad).ToString(),values.yetki.ToString())));


                ////hazır kod



            }




        }

















    }
}
