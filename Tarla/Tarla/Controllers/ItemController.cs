using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tarla.Controllers
{



    [Route("[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {





        [HttpGet]
        [Route("trees")]
        public async Task<IActionResult> GetAllTrees()
        {

            Repos.ItemReposotory query = new Repos.ItemReposotory();
            var result = await query.FindAllTrees();

            if (result is null)
                return new NotFoundResult();
            else
            {

                return Ok(result);

            }



        }


        [HttpGet("{id:int}")]
        [Route("tree")]
        public async Task<IActionResult> GetOneTree(int id)
        {

            Repos.ItemReposotory query = new Repos.ItemReposotory();
            var result = await query.GetOneTree(id);

            if (result is null)
                return new NotFoundResult();
            else
            {

                return Ok(result);

            }



        }



        [HttpPost]
        [Route("addtrees")]
        public async Task<IActionResult> InsertAgaclar([FromBody] List<Models.Tree> Tree)  //from body şart değil
        {

            int id = -1;
            Repos.ItemReposotory query = new Repos.ItemReposotory();

            await query.InsertTrees( Tree);



            return Ok(id);


        }

        [HttpPost]
        [Route("deletetrees")]
        public async Task<IActionResult> DeleteAgac([FromBody] int refid)  //from body şart değil
        {

            int id = -1;
            Repos.ItemReposotory query = new Repos.ItemReposotory();


            await query.DeleteAgac(refid);


            return Ok(refid);




        }





        [HttpPost]
        [Route("updateagacdurumu")]
        public async Task<IActionResult> UpdateAgacDurumu([FromBody] Models.agacdurumu agacdurumu)  //from body şart değil
        {

            int id = -1;
            Repos.ItemReposotory query = new Repos.ItemReposotory();

            await query.UpdateAgacDurumu(agacdurumu);

            return Ok();




        }




        [HttpPost]
        [Route("updateagacadaparselno")]
        public async Task<IActionResult> UpdateAgacAdaParselNo([FromBody] List<Models.adaparselno>  adaparselno)  //from body şart değil
        {

            int id = -1;
            Repos.ItemReposotory query = new Repos.ItemReposotory();

            await query.UpdateAgacParselAdaNo(adaparselno);


            return Ok();




        }




        /// <summary>
        /// //parsels
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [Route("parsels")]
        public async Task<IActionResult> GetAllParsels()
        {

            Repos.ItemReposotory query = new Repos.ItemReposotory();
            var result = await query.FindParselsWithListCoordinates();

            if (result is null)
                return new NotFoundResult();
            else
            {

                return Ok(result);

            }



        }

        [HttpPost]
        [Route("addparsels")]
        public async Task<IActionResult> InsertParseller([FromBody] List<Models.Parsel> Parsels)  //from body şart değil
        {

            int id = -1;
            Repos.ItemReposotory query = new Repos.ItemReposotory();

           await query.InsertParsels(Parsels);



            return Ok(id);


        }



        [HttpPost]
        [Route("updateparselmulkiyet")]
        public async Task<IActionResult> UpdateParsel([FromBody] Models.parselsahip mulkiyet)  //from body şart değil
        {

            int id = -1;
            Repos.ItemReposotory query = new Repos.ItemReposotory();

            await query.UpdateParselMulkiyet(mulkiyet);



            return Ok(id);


        }


        [HttpPost]
        [Route("deleteparsels")]
        public async Task<IActionResult> DeleteParsels([FromBody] DTO.TarlaSilmeDto silmeitemleri)  //from body şart değil
        {

            int id = -1;
            Repos.ItemReposotory query = new Repos.ItemReposotory();


            await query.DeleteParsel(silmeitemleri);


            return Ok(silmeitemleri);




        }



        /// <summary>
        /// //agaccinsleri
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [Route("agaccinsleri")]
        public async Task<IActionResult> GetAllAgacCinsleri()
        {

            Repos.ItemReposotory query = new Repos.ItemReposotory();
            var result = await query.FindAllAgaccinsi();

            if (result is null)
                return new NotFoundResult();
            else
            {

                return Ok(result);

            }



        }

        [HttpPost]
        [Route("addagaccinsi")]
        public async Task<IActionResult> InsertAgaccinsi([FromBody] Models.agaccinsleri agaccinsi)  //from body şart değil
        {

            int id = -1;
            Repos.ItemReposotory query = new Repos.ItemReposotory();

   


            return Ok(await query.Insertagaccinsi(agaccinsi));


        }

        [HttpPost]
        [Route("deleteagaccinsi")]
        public async Task<IActionResult> DeleteAgacCinsi([FromBody] int refid)  //from body şart değil
        {

            int id = -1;
            Repos.ItemReposotory query = new Repos.ItemReposotory();


          await query.DeleteAgacCinsi(refid);


            return Ok(refid);




        }


        [HttpPost]
        [Route("updateagaccinsi")]
        public async Task<IActionResult> UpdateAgacCinsi([FromBody] Models.agaccinsleri agaccinsi)  //from body şart değil
        {

            int id = -1;
            Repos.ItemReposotory query = new Repos.ItemReposotory();

          await query.UpdateAgacCinsi(agaccinsi);

            return Ok();




        }


        /// <summary>
        /// //agacturleri
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [Route("agacturleri")]
        public async Task<IActionResult> GetAllAgacTurleri()
        {

            Repos.ItemReposotory query = new Repos.ItemReposotory();
            var result = await query.FindAllAgacTurleri();

            if (result is null)
                return new NotFoundResult();
            else
            {

                return Ok(result);

            }

        }

        [HttpPost]
        [Route("addagacturu")]
        public async Task<IActionResult> InsertAgacturu([FromBody] Models.agacturleri agacturu)  //from body şart değil
        {

            int id = -1;
            Repos.ItemReposotory query = new Repos.ItemReposotory();




            return Ok(await query.Insertagacturleri(agacturu));


        }

        [HttpPost]
        [Route("deleteagacturu")]
        public async Task<IActionResult> DeleteAgacTuru([FromBody] int refid)  //from body şart değil
        {

            int id = -1;
            Repos.ItemReposotory query = new Repos.ItemReposotory();


            await query.DeleteAgacTuru(refid);


            return Ok(refid);




        }


        [HttpPost]
        [Route("updateagacturu")]
        public async Task<IActionResult> UpdateAgacTuru([FromBody] Models.agacturleri agacturleri)  //from body şart değil
        {

            int id = -1;
            Repos.ItemReposotory query = new Repos.ItemReposotory();

            await query.UpdateAgacTuru(agacturleri);

            return Ok();




        }





    }
}
