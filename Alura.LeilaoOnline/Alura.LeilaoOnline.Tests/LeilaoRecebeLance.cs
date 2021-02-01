using Alura.LeilaoOnline.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoRecebeLance
    {
        [Theory]
        [InlineData(4, new double[] { 100, 1200, 1400, 1300 })]
        [InlineData(2, new double[] { 800, 900 })]
        public void NaoPermiteNovosLancesDadoLeilaoFinalizado(int qtdeEsperada, double[] ofertas)
        {
            //Arranje - cenário
            IModalidadeAvaliacao modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);
            leilao.IniciaPregao();

            for (int i = 0; i < ofertas.Length; i++)
            {
                var valor = ofertas[i];
                if ((i % 2) == 0)
                {
                    leilao.RecebeLance(fulano, valor);
                }
                else
                {
                    leilao.RecebeLance(maria, valor);
                }
            }
            leilao.TerminaPregao();

            //Act - método sob teste
            leilao.RecebeLance(fulano, 1000);

            //Assert
            var qtdeObtida = leilao.Lances.Count();
            Assert.Equal(qtdeEsperada, qtdeObtida);
        }

        [Fact]
        public void NaoAceitaProximoLanceDadoMesmoClienteRealizouUltimoLance()
        {
            //Arrange - cenário
            IModalidadeAvaliacao modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh",modalidade);
            var fulano = new Interessada("Fulano", leilao);
            leilao.IniciaPregao();
            leilao.RecebeLance(fulano, 800);

            //Act - método sob teste
            leilao.RecebeLance(fulano, 1000);

            //Assert
            var qtdeEsperada = 1;
            var qtdeObtida = leilao.Lances.Count();
            Assert.Equal(qtdeEsperada, qtdeObtida);
        }
    }
}
