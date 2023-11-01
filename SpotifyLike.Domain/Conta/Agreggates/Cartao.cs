using SpotifyLike.Domain.Transacao.Agreggates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Domain.Conta.Agreggates
{
    public class Cartao
    {

        private const int TRANSACTION_TIME_INTERVAL = -2;
        private const int TRANSACTION_MERCHANT_REPEAT = 2;

        public Guid Id { get; set; }
        public Boolean Ativo { get; set; }
        public Decimal Limite { get; set; }
        public String Numero { get; set; }
        public List<Transacao.Agreggates.Transacao> Transacoes { get; set; }

        public Cartao()
        {
            this.Transacoes = new List<Transacao.Agreggates.Transacao>();
        }

        public void CriarTransacao(string merchant, Decimal valor, string descricao)
        {
            //Verificar se o cartao está ativo
            this.IsCartaoAtivo();

            var transacao = new Transacao.Agreggates.Transacao();
            transacao.Merchant = new Transacao.ValueObject.Merchant() {  Nome = merchant } ;
            transacao.Valor = valor ;
            transacao.Descricao = descricao ;
            transacao.DtTransacao = DateTime.Now;

            //Verificar o Limite
            this.VerificaLimiteDisponivel(transacao);
            
            //Validar a transação
            this.ValidarTransacao(transacao);

            //Criar numero de Autorização
            transacao.Id = Guid.NewGuid();

            //Adicionar uma nova transação
            this.Transacoes.Add(transacao);
        }

        private void IsCartaoAtivo()
        {
            if (this.Ativo == false)
            {
                //Todo: Criar Exceção de Négocio
            }

        }

        private void VerificaLimiteDisponivel(Transacao.Agreggates.Transacao transacao)
        {
            if (transacao.Valor > this.Limite)
            {
                //Todo: Criar exceção de Négocio
            }
        }

        private void ValidarTransacao(Transacao.Agreggates.Transacao transacao)
        {
            var ultimasTransacoes = this.Transacoes.Where(x => x.DtTransacao >= DateTime.Now.AddMinutes(TRANSACTION_TIME_INTERVAL));

            if (ultimasTransacoes?.Count() >= 3)
            {
                //TODO: CRIAR EXCEÇÃO DE NEGOCIO
            }

            if (ultimasTransacoes?.Where(x => x.Merchant.Nome.ToUpper() == transacao.Merchant.Nome.ToUpper() 
                                         &&   x.Valor == transacao.Valor).Count() == TRANSACTION_MERCHANT_REPEAT)
            {
                //TODO: CRIAR EXCEÇÃO DE NEGOCIO
            }
        }
    }
}
