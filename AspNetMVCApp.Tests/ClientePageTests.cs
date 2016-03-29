using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Tests.Pages;

namespace WebApplication2.Tests
{
    [TestClass]
    public class ClientePageTests : SeleniumTest
    {
        public ClientePageTests() : base("WebApplication2") { }

        [TestMethod]
        public void DeveExibirErroSeMenorDeIdade()
        {
            ClientePage criarCliente = new ClientePage(this.ChromeDriver, GetAbsoluteUrl("/cliente/create"));

            var erros = criarCliente.CriarCliente("Gerson", "Dias", 11);

            CollectionAssert.Contains(erros.ToArray(), "Cliente deve ser maior de idade");
        }

        private ClientePage _page = null;

        [TestMethod]
        public void ClienteNaoDeveSerMenorDeIdade()
        {
            DadaAPaginaDeCadastroDeCliente()
                .AoPreencherOPrimeiroNome("Gerson")
                .SegundoNome("Dias")
                .Idade(11)
                .EhExibidaAMensagemDaIdade("Cliente deve ser maior de idade");
        }

        private ClientePageTests DadaAPaginaDeCadastroDeCliente()
        {
            _page = new ClientePage(this.ChromeDriver, GetAbsoluteUrl("/cliente/create"));
            return this;
        }

        private ClientePageTests AoPreencherOPrimeiroNome(string nome)
        {
            _page.FirstName.SendKeys(nome);
            return this;
        }

        private ClientePageTests SegundoNome(string nome)
        {
            _page.LastName.SendKeys(nome);
            return this;
        }

        private ClientePageTests Idade(int idade)
        {
            _page.Idade.SendKeys(idade.ToString());
            _page.Idade.SendKeys(Keys.Tab);

            return this;
        }

        private void EhExibidaAMensagemDaIdade(string mensagem)
        {
            if (_page.Exists(By.XPath("//span[@for='Idade']")))
            {
                Assert.AreEqual(mensagem, _page.IdadeError.Text);
            }
        }
    }
}
