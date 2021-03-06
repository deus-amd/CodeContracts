// CodeContracts
// 
// Copyright (c) Microsoft Corporation
// 
// All rights reserved. 
// 
// MIT License
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED *AS IS*, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections;
using System.Text;
using System.Diagnostics.Contracts;

#if NETFRAMEWORK_3_5
namespace System.Threading.Tasks {
  public delegate T Func<T>();

  public class Task<T> {
      T value;
      public Task(Func<T> make) {
        value = make();
      }
      public void Wait() {}
      public void Start() {}
      public T Result { get { return this.value; } }
  }
}
#endif

namespace Tests.Sources
{

  using System.Threading.Tasks;


  partial class TestMain
  {

#if NETFRAMEWORK_4_5
    public static async Task DoWork(IEnumerable enumerable)
    {
        Contract.Requires(enumerable != null);
    }
#else
    public static Task<string> DoWork(IEnumerable enumerable)
    {
        Contract.Requires(enumerable != null);
        return new Task<string>(() => "");
    }
#endif
    
    void Test(int[] ms)
    {
      DoWork(ms);
    }

    partial void Run()
    {
      if (behave)
      {
        this.Test(new[]{50,30,10});
      }
      else
      {
        this.Test(null);
      }
    }

    public ContractFailureKind NegativeExpectedKind = ContractFailureKind.Precondition;
    public string NegativeExpectedCondition = "enumerable != null";
  }
}
