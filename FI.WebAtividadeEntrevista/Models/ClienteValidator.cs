using System.Linq;
using FI.AtividadeEntrevista.BLL;
using FluentValidation;

namespace WebAtividadeEntrevista.Models
{
    public class ClienteValidator : AbstractValidator<ClienteModel>
    {
        public ClienteValidator()
        {
            var bo = new BoCliente();
            RuleFor(model => model.CPF).Must(s => 11 <= s.Length && s.Length <= 14).WithMessage("Digite apenas os números.");
            RuleFor(model => model.CPF).Must(cpf => !bo.VerificarExistencia(cpf)).WithMessage("CPF já cadastrado.");
            RuleFor(model => model.CPF).Must(IsCpfValid).WithMessage("CPF inválido");
        }

        private static bool IsCpfValid(string cpf)
        {
            var multiplicador1 = new [] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new [] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            
            if (cpf.Length != 11)
                return false;

            if (cpf.Distinct().Count() == 1)
                return false;

            int d1Digitado;
            int.TryParse(cpf[(cpf.Length) - 2].ToString(), out d1Digitado);
            int d2Digitado;
            int.TryParse(cpf[(cpf.Length) - 1].ToString(), out d2Digitado);

            var soma = 0;
            for(var i = 0; i < 9; i++)
            {
                int digito;
                if (!int.TryParse(cpf[i].ToString(), out digito))
                    return false;
                soma += digito * multiplicador1[i];
            }

            var resto = soma % 11;
            int d1;
            if (resto < 2 )
                d1 = 0;
            else
                d1 = 11 - resto;

            if (d1 != d1Digitado)
                return false;
            
            soma = 0;
            for(var i = 0; i < 10; i++)
            {
                int digito;
                if (!int.TryParse(cpf[i].ToString(), out digito))
                    return false;
                soma += digito * multiplicador2[i];
            }

            var resto2 = soma % 11;
            int d2;
            if (resto2 < 2)
                d2 = 0;
            else
                d2 = 11 - resto2;
            
            return d2 == d2Digitado;
        }
    }
    
}