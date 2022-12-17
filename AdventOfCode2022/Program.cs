using System.Diagnostics;

var sw = Stopwatch.StartNew();
Day17.Run1().Log("Step 1: ");
Day17.Run2().Log("Step 2: ");
$"Duration: {sw.ElapsedMilliseconds} ms".Log();
