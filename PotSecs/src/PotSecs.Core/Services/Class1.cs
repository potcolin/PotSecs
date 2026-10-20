using Microsoft.Extensions.Options;
using Secs4Net;
using System;
using System.Collections.Generic;
using System.Text;

namespace PotSecs.Core.Services
{
    public class Class1
    {


        public async Task AreYouThereAsync()
        {
            using var request = new SecsMessage(1, 1)
            {
                Name = "AreYouThere"
            };
            using var secsGem = new SecsGem(null, null, null);
            try
            {
                using var reply = await secsGem.SendAsync(request, new CancellationToken());

                Console.WriteLine($"收到应答：{reply}");
            }
            catch (SecsException ex)
            {
                Console.WriteLine($"SECS 通信异常：{ex.Message}");
            }
        }
    }
}
