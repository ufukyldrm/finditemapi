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




using Microsoft.IdentityModel.Tokens;

using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;








namespace Tarla.Repos
{
    public class AuthReposotory
    {


        static MySqlConnection connection = new MySqlConnection("server=94.73.149.62;user id=ktaki_ufukyldrm;pwd=u20322817;database=ktakip_site_tarla;Persist Security Info=False;AllowZeroDateTime=True");


        static string connectionString = "server=94.73.149.62;user id=ktaki_ufukyldrm;pwd=u20322817;database=ktakip_site_tarla;Persist Security Info=False;AllowZeroDateTime=True";




        public async Task<int> InsertUser(Models.kullanicilar kullanicilar)
        {

            SHA1 sha = new SHA1CryptoServiceProvider();

            string hash = Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(kullanicilar.sifre)));

            string salt = Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(hash + kullanicilar.sifre)));



            StringBuilder sCommand = new StringBuilder(@"INSERT INTO `kullanicilar` (`ad`, `sifre`,`passwordhash`, `passwordsalt`, `yetki`) VALUES ('" + kullanicilar.ad + "', '" + kullanicilar.sifre + "','" +hash + "', '" + salt + "','" + kullanicilar.yetki + "');");
            using (MySqlConnection mConnection = new MySqlConnection(connectionString))
            {
                await mConnection.OpenAsync();

                List<string> Rows = new List<string>();



                using (MySqlCommand myCmd = new MySqlCommand(sCommand.ToString(), mConnection))
                {
                    myCmd.CommandType = CommandType.Text;
                    await myCmd.ExecuteNonQueryAsync();

                    await mConnection.CloseAsync();

                    return (int)myCmd.LastInsertedId;
                }
                await mConnection.CloseAsync();
            }


        }


        public async Task<List<kullanicilar>> FindAllPersons()
        {





            var posts = new List<kullanicilar>();

            await connection.OpenAsync();

            using var command = new MySqlCommand(@"SELECT * FROM `kullanicilar`", connection);
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var post = new kullanicilar()
                {
                    id = reader.GetInt32(0),
                    ad= reader.GetString(1),
                    sifre=       reader.GetString(2),
                   passwordhash = reader.GetString(3),
                    passwordsalt = reader.GetString(4),
                    yetki = reader.GetInt32(5),

                };
                posts.Add(post);



                // do something with 'value'
            }

            await connection.CloseAsync();

            return posts;



        }












        public async Task<List<DTO.YetkiDto>> FindAllYetki()
        {





            var posts = new List<DTO.YetkiDto>();

            await connection.OpenAsync();

            using var command = new MySqlCommand(@"SELECT * FROM `kullanicilar`", connection);
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {






                bool duzenleyici = false;
                bool izleyici = false;
                bool admin = false;


                if (reader.GetInt32("yetki") == 0)
                {




                    izleyici = true;



                }

                else if (reader.GetInt32("yetki") == 1)
                {




                    duzenleyici = true;



                }

                if (reader.GetInt32("yetki") == 2)
                {




                    admin = true;

                }




                var post = new DTO.YetkiDto()
                    {
                        id = reader.GetInt32(0),
                        mail = reader.GetString(1),
                        admin = admin,
                        duzenleyici = duzenleyici,
                        izleyici = izleyici



                    };
                    posts.Add(post);



                    // do something with 'value'
               
            }

            await connection.CloseAsync();

                return posts;



       
        }








        public async Task UpdateYetki(DTO.YetkiDto yetki)
        {


            bool duzenleyici = false;

            bool admin = false;

            bool izleyici = false;

            int yetkino = 0;




            if (yetki.admin == true)
                yetkino = 2;

           else if (yetki.duzenleyici == true)
                yetkino = 1;

            if (yetki.izleyici == true)
                yetkino = 0;


            string querrycombined = "";


            querrycombined = "update kullanicilar set yetki='" + yetkino + "' where id=" + yetki.id + "; ";



            MySqlConnection conn = null;
            MySqlTransaction tr = null;

            try
            {
                conn = new MySqlConnection(connectionString);
                await conn.OpenAsync();



                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;


                cmd.CommandText = querrycombined;
                await cmd.ExecuteNonQueryAsync();



            }
            catch (MySqlException ex)
            {

            }
            finally
            {
                if (conn != null)
                    await conn.CloseAsync();
            }



        }


        public async Task<kullanicilar> Login(string username, string password)
        {

            if (await UserExist(username))
            {
                SHA1 sha = new SHA1CryptoServiceProvider();


                //    var user = _context.Users.FirstOrDefault(u => u.UserName == username);



                var user = await FindAllPersons();

                var user2 = from kullanciadi in user
                           where kullanciadi.ad == username
                           select kullanciadi;


                List<Models.kullanicilar> kullanıcılar = new List<kullanicilar>();


                kullanıcılar = user2.ToList();




                string hash = Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(password)));

                string salt = Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(hash + password)));

                if (kullanıcılar[0].passwordhash == hash && kullanıcılar[0].passwordsalt == salt)
                {
                    return kullanıcılar[0];

                }
                else
                    return null;

            }
            else  //şifre kontrolü
            {


                return null;

            }


        }

        public async Task<bool> UserExist(string username)
        {

            bool varmı = false;




            var user = await FindAllPersons();


            varmı = user.Any(c => c.ad == username);

            return varmı;


        }



        public async Task<int> NameIdBul(string name)
        {



            var user = await FindAllPersons();




            var values = from kullanici in user
                         where kullanici.ad == name
                         select kullanici;


            List<kullanicilar> cekilenkullanicilaridler = new List<kullanicilar>();


            cekilenkullanicilaridler = values.ToList();


            return cekilenkullanicilaridler[0].id;


        }


        public string GenerateToken(string userName, string userid, string yetki)
        {
            ////hazır kod

            var someClaims = new Claim[]{
                new Claim(JwtRegisteredClaimNames.GivenName,userName),
                new Claim(JwtRegisteredClaimNames.NameId,userid),
                   new Claim(JwtRegisteredClaimNames.Acr,yetki),


            };

            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("uzun ince bir yoldayım şarkısını buradan tüm sevdiklerime hediye etmek istiyorum mümkün müdür acaba?"));
            var token = new JwtSecurityToken(
                issuer: "west-world.fabrikam.com",
                audience: "www.ufukyildirim.com",
                claims: someClaims,
                expires: DateTime.Now.AddMinutes(3),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);

            ////hazır kod
            ///



        }













    }
}
