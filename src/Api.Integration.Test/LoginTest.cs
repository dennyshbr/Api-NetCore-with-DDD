using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.Integration.Test
{
    public class LoginTest : BaseIntegration
    {
        [Fact]
        public async Task GenerationToken()
        {
            await AddToken();
        }
    }
}
