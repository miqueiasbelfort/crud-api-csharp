using Microsoft.EntityFrameworkCore;
using WebAPI_Funcionarios.DataContext;
using WebAPI_Funcionarios.Models;

namespace WebAPI_Funcionarios.Service.FuncionarioService
{
    public class FuncionarioService : IFuncionarioInterface
    {
        private readonly ApplicationDbContext _context;
        public FuncionarioService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Pegar todos os funcionarios e retornar-los dentro de uma model ServiceResponse
        public async Task<ServiceResponse<List<FuncionarioModel>>> GetFuncionarios()
        {
            ServiceResponse<List<FuncionarioModel>> serviceResponse = new ServiceResponse<List<FuncionarioModel>>();
            
            try
            {
                serviceResponse.Dados = _context.Funcionarios.ToList();

                if(serviceResponse.Dados.Count == 0)
                {
                    serviceResponse.Messagem = "Nenhum dado encontrado";
                }
            }
            catch(Exception ex)
            {
                serviceResponse.Messagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        
        }

        public async Task<ServiceResponse<List<FuncionarioModel>>> CreateFuncionario(FuncionarioModel novoFuncionario)
        {
            ServiceResponse<List<FuncionarioModel>> serviceResponse = new ServiceResponse<List<FuncionarioModel>>();

            try
            {
                if (novoFuncionario == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Messagem = "Informar Dados";
                    serviceResponse.Sucesso = false;

                    return serviceResponse;
                }

                // Criar novo funcionario
                _context.Add(novoFuncionario);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = _context.Funcionarios.ToList();


            }
            catch (Exception ex)
            {
                serviceResponse.Messagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<FuncionarioModel>> GetFuncionarioById(int id)
        {
            ServiceResponse<FuncionarioModel> serviceResponse = new ServiceResponse<FuncionarioModel>();
        
            try
            {
                FuncionarioModel funcionario = _context.Funcionarios.FirstOrDefault(x => x.Id == id);

                if (funcionario == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Messagem = "Funcionario não localizado";
                    serviceResponse.Sucesso = false;

                    return serviceResponse;
                }
                
                serviceResponse.Dados = funcionario;

            }
            catch (Exception ex)
            {
                serviceResponse.Messagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        // Está tendo algum erro no update
        public async Task<ServiceResponse<List<FuncionarioModel>>> UpdateFuncionario(FuncionarioModel editadoFuncionario)
        {
            ServiceResponse<List<FuncionarioModel>> serviceResponse = new ServiceResponse<List<FuncionarioModel>>();

            try
            {
                FuncionarioModel funcionario = _context.Funcionarios.AsNoTracking().FirstOrDefault(x => x.Id == editadoFuncionario.Id);
                if (funcionario == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Messagem = "Funcionarios não encontrado";
                    serviceResponse.Sucesso = false;
                    return serviceResponse;
                }

                editadoFuncionario.DataDeAlteracao = DateTime.Now.ToLocalTime();
                _context.Funcionarios.Update(editadoFuncionario);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = _context.Funcionarios.ToList();

            }
            catch (Exception ex)
            {
                serviceResponse.Messagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<FuncionarioModel>>> DeleteFuncionario(int id)
        {
            ServiceResponse<List<FuncionarioModel>> serviceResponse = new ServiceResponse<List<FuncionarioModel>>();

            try
            {
                FuncionarioModel funcionario = _context.Funcionarios.FirstOrDefault(x => x.Id == id);
                if (funcionario == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Messagem = "Funcionario não encontrado!";
                    serviceResponse.Sucesso = false;
                    return serviceResponse;
                }

                _context.Funcionarios.Remove(funcionario);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = _context.Funcionarios.ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Messagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<FuncionarioModel>>> InativarFuncionario(int id)
        {
            ServiceResponse<List<FuncionarioModel>> serviceResponse = new ServiceResponse<List<FuncionarioModel>>();

            try
            {

                FuncionarioModel funcionario = _context.Funcionarios.FirstOrDefault(x => x.Id == id);
                if (funcionario == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Messagem = "Funcionarios não encontrado";
                    serviceResponse.Sucesso = false;
                    return serviceResponse;
                }

                funcionario.Ativo = false;
                funcionario.DataDeAlteracao = DateTime.Now.ToLocalTime();

                _context.Funcionarios.Update(funcionario);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = _context.Funcionarios.ToList();

            }
            catch (Exception ex)
            {
                serviceResponse.Messagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;

        }
    }
}
