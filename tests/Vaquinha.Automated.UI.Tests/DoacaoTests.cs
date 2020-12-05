using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using Vaquinha.Tests.Common.Fixtures;
using Xunit;

namespace Vaquinha.AutomatedUITests
{
	public class DoacaoTests : IDisposable, IClassFixture<DoacaoFixture>, 
                                               IClassFixture<EnderecoFixture>, 
                                               IClassFixture<CartaoCreditoFixture>
	{
		private DriverFactory _driverFactory = new DriverFactory();
		private IWebDriver _driver;

		private readonly DoacaoFixture _doacaoFixture;
		private readonly EnderecoFixture _enderecoFixture;
		private readonly CartaoCreditoFixture _cartaoCreditoFixture;

		public DoacaoTests(DoacaoFixture doacaoFixture, EnderecoFixture enderecoFixture, CartaoCreditoFixture cartaoCreditoFixture)
        {
            _doacaoFixture = doacaoFixture;
            _enderecoFixture = enderecoFixture;
            _cartaoCreditoFixture = cartaoCreditoFixture;
        }
		public void Dispose()
		{
			_driverFactory.Close();
		}

		[Fact]
		public void DoacaoUI_AcessoTelaHome()
		{
			// Arrange
			_driverFactory.NavigateToUrl("https://vaquinha.azurewebsites.net/");
			_driver = _driverFactory.GetWebDriver();

			// Act
			IWebElement webElement = null;
			webElement = _driver.FindElement(By.ClassName("vaquinha-logo"));

			// Assert
			webElement.Displayed.Should().BeTrue(because:"logo exibido");
		}
		[Fact]
		public void DoacaoUI_CriacaoDoacao()
		{
			//Arrange
			var doacao = _doacaoFixture.DoacaoValida();
            doacao.AdicionarEnderecoCobranca(_enderecoFixture.EnderecoValido());
            doacao.AdicionarFormaPagamento(_cartaoCreditoFixture.CartaoCreditoValido());
			_driverFactory.NavigateToUrl("https://vaquinha.azurewebsites.net/");
			_driver = _driverFactory.GetWebDriver();

			//Act
			IWebElement webElement = null;
			webElement = _driver.FindElement(By.ClassName("btn-yellow"));
			webElement.Click();

			IWebElement campoNome = _driver.FindElement(By.Id("DadosPessoais_Nome"));
			campoNome.SendKeys(doacao.DadosPessoais.Nome);

			IWebElement campoEmail = _driver.FindElement(By.Id("DadosPessoais_Email"));
			campoNome.SendKeys(doacao.DadosPessoais.Email);

			IWebElement campoEnderecoCobranca = _driver.FindElement(By.Id("EnderecoCobranca_TextoEndereco"));
			campoNome.SendKeys(doacao.EnderecoCobranca.TextoEndereco);

			IWebElement campoNumero = _driver.FindElement(By.Id("EnderecoCobranca_Numero"));
			campoNome.SendKeys(doacao.EnderecoCobranca.Numero);

			IWebElement campoCidade = _driver.FindElement(By.Id("EnderecoCobranca_Cidade"));
			campoNome.SendKeys(doacao.EnderecoCobranca.Cidade);

			IWebElement campoEstado = _driver.FindElement(By.Id("estado"));
			campoNome.SendKeys(doacao.EnderecoCobranca.Estado);

			IWebElement campoCEP = _driver.FindElement(By.Id("cep"));
			campoNome.SendKeys(doacao.EnderecoCobranca.CEP);

			IWebElement campoComplemento = _driver.FindElement(By.Id("EnderecoCobranca_Complemento"));
			campoNome.SendKeys(doacao.EnderecoCobranca.Complemento);

			IWebElement campoTelefone = _driver.FindElement(By.Id("telefone"));
			campoNome.SendKeys(doacao.EnderecoCobranca.Telefone);

			//Assert
			_driver.Url.Should().Contain("/Doacoes/Create");
		}
	}
}