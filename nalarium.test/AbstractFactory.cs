#region Copyright

// MIT License

// Copyright (c) 2007-2017 David Betz

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

#endregion

using Xunit;
using System;

namespace Nalarium.Test
{
    public interface IMockProvider
    {
        string Execute(string exampleRequiredParameter, params string[] parameterArray);
    }

    public class MockProvider : IMockProvider
    {
        public string Execute(string exampleRequiredParameter, params string[] parameterArray)
        {
            return $"{exampleRequiredParameter}mock provider";
        }
    }

    public class AlternativeMockProvider : IMockProvider
    {
        private string _param;
        public AlternativeMockProvider(string param)
        {
            _param = param;
        }
        public string Execute(string exampleRequiredParameter, params string[] parameterArray)
        {
            return $"{exampleRequiredParameter}{_param}alternative mock provider";
        }
    }

    public class MockProviderFactory : IProviderFactory<IMockProvider>
    {
        public IMockProvider Create(params string[] parameterArray)
        {
            string hint = null;
            if (parameterArray.Length > 0)
            {
                hint = parameterArray[0].ToLower();
            }

            var param = string.Empty;
            if (parameterArray.Length > 1)
            {
                param = parameterArray[1].ToLower();
            }

            var providerToUseFromConfigStore = "mock";

            IMockProvider provider = null;

            var name = (hint ?? providerToUseFromConfigStore ?? string.Empty).ToLower();
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            if (name == "mock")
            {
                provider = new MockProvider();
            }
            else if (name == "alt")
            {
                provider = new AlternativeMockProvider(param);
            }
            else
            {
                throw new InvalidOperationException("no");
            }
            return provider;
        }
    }

    
    public class AbstractFactory : IDisposable
    {
        public AbstractFactory()
        {
            Nalarium.AbstractFactory.Set(new MockProviderFactory());
        }

        [Fact]
        public void RunFromConfig()
        {
            var provider = Nalarium.AbstractFactory.Resolve<IMockProvider>();
            var result = provider.Execute("hello");

            Assert.Equal(result, "hellomock provider");
        }

        [Fact]
        public void RunWithOverride()
        {
            var provider = Nalarium.AbstractFactory.Resolve<IMockProvider>("alt", "parameter for alt");
            var result = provider.Execute("hi");

            Assert.Equal(result, "hiparameter for altalternative mock provider");
        }

        public void Dispose()
        {
            Nalarium.AbstractFactory.Remove<IMockProvider>();
        }
    }
}
