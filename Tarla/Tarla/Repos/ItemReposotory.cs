using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Data;

using Tarla.Models;

using System.Data.Common;
using MySqlConnector;
using System.Text;

namespace Tarla.Repos
{
    public class ItemReposotory
    {

       
        static MySqlConnection connection= new MySqlConnection("server=94.73.149.62;user id=ktaki_ufukyldrm;pwd=u20322817;database=ktakip_site_tarla;Persist Security Info=False;AllowZeroDateTime=True");


        static string connectionString = "server=94.73.149.62;user id=ktaki_ufukyldrm;pwd=u20322817;database=ktakip_site_tarla;Persist Security Info=False;AllowZeroDateTime=True";



        public async Task<List<Tree>> FindAllTrees()
        {


         


            var posts = new List<Tree>();

            await connection.OpenAsync();

            using var command = new MySqlCommand(@"SELECT * FROM `agac`", connection);
            using var reader = await command.ExecuteReaderAsync();
            
            while (await reader.ReadAsync())
            {
                var post = new Tree()
                {
                    id = reader.GetInt32(0),
                    sırano= reader.GetInt32(1),
                   agacno = reader.GetInt32(2),
                    agaccinsi = reader.GetString(3),
                    agacturu = reader.GetString(4),
                    dikimyili = reader.GetInt32(5),
                    adano = reader.GetInt32(6),
                    parselno = reader.GetInt32(7),

                   latcoordinat = reader.GetDecimal(8),
                    longcoordinat = reader.GetDecimal(9),


                };
                posts.Add(post);



                // do something with 'value'
            }

            await connection.CloseAsync();

            return posts;



        }



        public async Task<Tree> GetOneTree(int id)
        {





            var post = new Tree();

            await connection.OpenAsync();

            using var command = new MySqlCommand(@"SELECT * FROM `agac` where id="+id+"", connection);
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                post = new Tree()
                {
                    id = reader.GetInt32(0),
                    sırano = reader.GetInt32(1),
                    agacno = reader.GetInt32(2),
                    agaccinsi = reader.GetString(3),
                    agacturu = reader.GetString(4),
                    dikimyili = reader.GetInt32(5),
                    adano = reader.GetInt32(6),
                    parselno = reader.GetInt32(7),

                    latcoordinat = reader.GetDecimal(8),
                    longcoordinat = reader.GetDecimal(9),

                    
                };
              



                // do something with 'value'
            }

            await connection.CloseAsync();

            return post;



        }


        public async Task DeleteAgac(int id)
        {



            MySqlConnection connection = null;
            try
            {
                connection = new MySqlConnection(connectionString);
                await connection.OpenAsync();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "DELETE FROM agac WHERE id= " + id + "; ";
                await cmd.ExecuteNonQueryAsync();
            }
            finally
            {
                if (connection != null)
                    await connection.CloseAsync();
            }




        }


        public async Task UpdateAgacDurumu(agacdurumu agacdurumu)
        {


            string querrycombined = "";


            querrycombined = "update agac set agacno='" + agacdurumu.agacdurumlari + "' where id=" + agacdurumu.id+ "; ";


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

        public async Task UpdateAgacParselAdaNo(List<adaparselno> adaparselno)
        {


            string querrycombined = "";


            querrycombined = "";


            for(int i=0;i<adaparselno.Count;i++)
            querrycombined = querrycombined+ "update agac set parselno='" + adaparselno[i].parselno + "',adano='" + adaparselno[i].adano + "' where id=" +adaparselno[i].id + "; ";



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



        public async Task InsertTrees(List<Tree> agaclar)
        {






   
            StringBuilder sCommand = new StringBuilder("INSERT INTO agac (sırano,agacno,agaccinsi,agacturu,dikimyili,adano,parselno,latcoordinat,longcoordinat) VALUES ");
            using (MySqlConnection mConnection = new MySqlConnection(connectionString))
            {
             await   mConnection.OpenAsync();

                List<string> Rows = new List<string>();
                for (int i = 0; i < agaclar.Count; i++)
                {
                    Rows.Add(string.Format("('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')",
                        MySqlHelper.EscapeString(agaclar[i].sırano.ToString()),
                        MySqlHelper.EscapeString(agaclar[i].agacno.ToString()),
                        MySqlHelper.EscapeString(agaclar[i].agaccinsi.ToString()),
                        MySqlHelper.EscapeString(agaclar[i].agacturu.ToString()),
                        MySqlHelper.EscapeString(agaclar[i].dikimyili.ToString()),
                        MySqlHelper.EscapeString(agaclar[i].adano.ToString()),
                        MySqlHelper.EscapeString(agaclar[i].parselno.ToString()),
                        MySqlHelper.EscapeString(agaclar[i].latcoordinat.ToString()),
                           MySqlHelper.EscapeString(agaclar[i].longcoordinat.ToString())


                        ));
                }
                sCommand.Append(string.Join(",", Rows));
                sCommand.Append(";");

                using (MySqlCommand myCmd = new MySqlCommand(sCommand.ToString(), mConnection))
                {
                    myCmd.CommandType = CommandType.Text;
                    await myCmd.ExecuteNonQueryAsync();
                  //  Id = (int)myCmd.LastInsertedId;
                }
                await mConnection.CloseAsync();
            }
            

        }













        /// <summary>
        /// /
        /// </summary>
        ///agaccinsleri
        /// <returns></returns>

        public async Task<int> Insertagaccinsi(agaccinsleri agaccinsi)
        {




            StringBuilder sCommand = new StringBuilder("INSERT INTO agaccinsleri (agaccinsi) VALUES ('"+agaccinsi.agaccinsi+"')");
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



        public async Task<List<agaccinsleri>> FindAllAgaccinsi()
        {





            var posts = new List<agaccinsleri>();

            await connection.OpenAsync();

            using var command = new MySqlCommand(@"SELECT * FROM `agaccinsleri`", connection);
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var post = new agaccinsleri()
                {
                    id = reader.GetInt32(0),
                    agaccinsi = reader.GetString(1),
     


                };

                posts.Add(post);



                // do something with 'value'
            }

            await connection.CloseAsync();

            return posts;



        }


         public async Task DeleteAgacCinsi(int id)
        {



            MySqlConnection connection = null;
            try
            {
                connection = new MySqlConnection(connectionString);
                await connection.OpenAsync();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "DELETE FROM agaccinsleri WHERE id= " + id + "; ";
            await    cmd.ExecuteNonQueryAsync();
            }
            finally
            {
                if (connection != null)
                    await connection.CloseAsync();
            }




        }

   


        public async Task UpdateAgacCinsi(agaccinsleri agaccinsi)
        {


            string querrycombined = "";


                querrycombined ="update agaccinsleri set agaccinsi='" + agaccinsi.agaccinsi + "' where id=" + agaccinsi.id + "; ";



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






        /// <summary>
        /// /
        /// </summary>
        ///agacturleri
        /// <returns></returns>

        public async Task<int> Insertagacturleri(agacturleri agacturu)
        {




            StringBuilder sCommand = new StringBuilder("INSERT INTO agacturleri (agacturu) VALUES ('" + agacturu.agacturu + "')");
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



        public async Task<List<agacturleri>> FindAllAgacTurleri()
        {





            var posts = new List<agacturleri>();

            await connection.OpenAsync();

            using var command = new MySqlCommand(@"SELECT * FROM `agacturleri`", connection);
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var post = new agacturleri()
                {
                    id = reader.GetInt32(0),
                   agacturu = reader.GetString(1),



                };

                posts.Add(post);



                // do something with 'value'
            }

            await connection.CloseAsync();

            return posts;



        }


        public async Task DeleteAgacTuru(int id)
        {



            MySqlConnection connection = null;
            try
            {
                connection = new MySqlConnection(connectionString);
                await connection.OpenAsync();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "DELETE FROM agacturleri WHERE id= " + id + "; ";
                await cmd.ExecuteNonQueryAsync();
            }
            finally
            {
                if (connection != null)
                    await connection.CloseAsync();
            }




        }




        public async Task UpdateAgacTuru(agacturleri agacturu)
        {


            string querrycombined = "";


            querrycombined = "update agacturleri set agacturu='" + agacturu.agacturu + "' where id=" + agacturu.id + "; ";



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






















        /// <summary>
        /// ///parsels
        /// </summary>
        /// <returns></returns>

        public async Task InsertParsels(List<Parsel> parseller)
        {




            StringBuilder sCommand = new StringBuilder("INSERT INTO tarla (adano2,parselno2,satınalmadurumu,lng,lat) VALUES ");
            using (MySqlConnection mConnection = new MySqlConnection(connectionString))
            {
                await mConnection.OpenAsync();

                List<string> Rows = new List<string>();
                for (int i = 0; i < parseller.Count; i++)
                {
                    Rows.Add(string.Format("('{0}','{1}','{2}','{3}','{4}')",
                        MySqlHelper.EscapeString(parseller[i].adano2.ToString()),
                        MySqlHelper.EscapeString(parseller[i].parselno2.ToString()),
                        MySqlHelper.EscapeString(parseller[i].satınalmadurumu.ToString()),
                        MySqlHelper.EscapeString(parseller[i].lng.ToString()),
                        MySqlHelper.EscapeString(parseller[i].lat.ToString())



                        ));
                }
                sCommand.Append(string.Join(",", Rows));
                sCommand.Append(";");

                using (MySqlCommand myCmd = new MySqlCommand(sCommand.ToString(), mConnection))
                {
                    myCmd.CommandType = CommandType.Text;
                    await myCmd.ExecuteNonQueryAsync();
                    //  Id = (int)myCmd.LastInsertedId;
                }
                await mConnection.CloseAsync();
            }


        }




        public async Task UpdateParselMulkiyet(parselsahip parselsahip)
        {


            string querrycombined = "";



                querrycombined = querrycombined + "update tarla set satınalmadurumu='" + parselsahip.satınalmadurumu+ "' where adano2=" + parselsahip.adano + " and parselno2=" + parselsahip.parselno + "; ";



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

















        public async Task<List<Parsel>>  FindAllParsels()
        {


            var parselcoordinats = new List<parselcoordinats>();


            var posts = new List<Parsel>();

            await connection.OpenAsync();

            using var command = new MySqlCommand(@"SELECT * FROM `tarla`", connection);
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var post = new Parsel()
                {
                    id = reader.GetInt32(0),
                    adano2 = reader.GetInt32(1),
                    parselno2 = reader.GetInt32(2),
                    satınalmadurumu = reader.GetString(3),
                    lng = reader.GetDecimal(4),
                    lat = reader.GetDecimal(5),


                };
                posts.Add(post);



                // do something with 'value'
            }

            await connection.CloseAsync();

            return posts;







        }
        public async Task DeleteParsel(DTO.TarlaSilmeDto silmeitemleri)
        {



            MySqlConnection connection = null;
            try
            {
                connection = new MySqlConnection(connectionString);
                await connection.OpenAsync();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "DELETE FROM tarla WHERE parselno2= " + silmeitemleri.parselno2 + " and adano2="+silmeitemleri.adano2+"; ";
                await cmd.ExecuteNonQueryAsync();
            }
            finally
            {
                if (connection != null)
                    await connection.CloseAsync();
            }




        }


        public async Task<List<DTO.ParselDTO>> FindParselsWithListCoordinates()
        {



            var parselalllist = new List<Parsel>();
            parselalllist = await FindAllParsels();



            var posts = new List<DTO.ParselDTO>();

            await connection.OpenAsync();

            using var command = new MySqlCommand(@"SELECT * FROM `tarla` group by parselno2,adano2", connection);
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var post = new DTO.ParselDTO()
                {

                    id = reader.GetInt32(0),
                    adano2 = reader.GetInt32(1),
                    parselno2 = reader.GetInt32(2),
                     satınalmadurumu = reader.GetString(3)



                };
                posts.Add(post);



                // do something with 'value'
            }

            await connection.CloseAsync();


            foreach (var parsel in posts)
            {

                parsel.parselcoordinats = (from n in parselalllist
                        where n.parselno2 == parsel.parselno2 && n.adano2 == parsel.adano2
                        select new parselcoordinats { lat = n.lat, lng = n.lng }).ToList();




                parsel.maincoordinates = GetCentroid(parsel.parselcoordinats);


                //    proje.projebasamaklari = result.Where(p => p.projekodu == proje.id.ToString()).ToList();  //projelerin projeleri
            }

            return posts;




        }

        public static parselcoordinats GetCentroid(List<parselcoordinats> poly)
        {
           decimal accumulatedArea = 0;
           decimal centerX = 0;
           decimal centerY = 0;

            for (int i = 0, j = poly.Count - 1; i < poly.Count; j = i++)
            {
                decimal temp = poly[i].lng * poly[j].lat - poly[j].lng * poly[i].lat;
                accumulatedArea = temp+ accumulatedArea;
                centerX += (poly[i].lng + poly[j].lng) * temp;
                centerY += (poly[i].lat + poly[j].lat) * temp;
            }

            if (Math.Abs(accumulatedArea) < 0)
                return null;  // Avoid division by zero

            accumulatedArea *= 3;
            return new parselcoordinats() { lng = centerX / accumulatedArea, lat=centerY / accumulatedArea };
        }





    }
}
