using Xunit;
using System;
using Alura.LeilaoOnline.Core;

namespace Alura.LeilaoOnline.Tests
{
    public class LanceCtor
    {
        [Fact]
        public void LancaArgumentExceptionDadoValorNegativo()
        {
            //Arrange - cenário
            var valorNegativo = -100;

            //Assert
            Assert.Throws<ArgumentException>(
                //Act
                () => new Lance(null, valorNegativo)
            );
        }
    }
}
