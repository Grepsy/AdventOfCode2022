using System.Diagnostics;

var sw = Stopwatch.StartNew();
Day16.Run1().Log("Step 1: ");
Day16.Run2().Log("Step 2: ");
$"Duration: {sw.ElapsedMilliseconds} ms".Log();
