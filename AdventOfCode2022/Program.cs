using System.Diagnostics;

var sw = Stopwatch.StartNew();
Day15.Run1().Log("Step 1: ");
Day15.Run2().Log("Step 2: ");
$"Duration: {sw.ElapsedMilliseconds} ms".Log();
