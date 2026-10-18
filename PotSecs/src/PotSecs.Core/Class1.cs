using Secs4Net;
using static Secs4Net.Item;
namespace PotSecs.Core
{
    public class Class1
    {

        public void TTT()
        {
            try
            {
                var s3f17 = new SecsMessage(3, 17)
                {
                    Name = "CreateProcessJob",
                    SecsItem = L(
                        U4(0),
                        L(
                            L(
                                A("Id"),
                                B(0x0D),
                                L(
                                    A("carrier id"),
                                    L(
                                        U1(1)),
                                    L(
                                        U1(1),
                                        A("recipe"),
                                        L()),
                                    Boolean(true),
                                    L()))))
                };

                //access list
                s3f17.SecsItem[1][0][0] = A("Id");

                foreach (var item in s3f17.SecsItem[1][0][2].Items)
                {

                }
            }
            catch (SecsException)
            {
                // exception  when
                // T3 timeout
                // device reply SxF0
                // device reply S9Fx
            }
        }
    }
}
