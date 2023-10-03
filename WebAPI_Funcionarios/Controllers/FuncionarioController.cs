using Microsoft.AspNetCore.Mvc;
using WebAPI_Funcionarios.Models;
using WebAPI_Funcionarios.Service.FuncionarioService;

namespace WebAPI_Funcionarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        private readonly IFuncionarioInterface _service;
        public FuncionarioController(IFuncionarioInterface funcionarioInterface)
        {
            _service = funcionarioInterface;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<FuncionarioModel>>>> GetFuncionarios()
        {
            return Ok( await _service.GetFuncionarios() );
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<FuncionarioModel>>> GetFuncionarioById(int id)
        {
            ServiceResponse<FuncionarioModel> serviceResponse = await _service.GetFuncionarioById(id);
            return Ok(serviceResponse);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<FuncionarioModel>>>> CreateFuncionario(FuncionarioModel novoFuncionario)
        {
            return Ok(await _service.CreateFuncionario(novoFuncionario));
        }

        [HttpPut("inativarFuncionario")]
        public async Task<ActionResult<ServiceResponse<List<FuncionarioModel>>>> InativarFuncionario(int id)
        {
            ServiceResponse<List<FuncionarioModel>> serviceResponse = await _service.InativarFuncionario(id);
            return Ok(serviceResponse);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<FuncionarioModel>>>> UpdateFuncionario(FuncionarioModel editadoFuncionario)
        {
            ServiceResponse<List<FuncionarioModel>> serviceResponse = await _service.UpdateFuncionario(editadoFuncionario);
            return Ok(serviceResponse);
        }


        [HttpDelete]
        public async Task<ActionResult<ServiceResponse<List<FuncionarioModel>>>> DeleteFuncionario(int id)
        {
            ServiceResponse<List<FuncionarioModel>> serviceResponse = await _service.DeleteFuncionario(id);
            return Ok(serviceResponse);
        }

    }
}
