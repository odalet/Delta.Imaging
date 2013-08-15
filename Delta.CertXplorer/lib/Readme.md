All libraries in this directory were recompiled to target .NET 4

* Weifen Luo's DockPanel was recompiled from sources by simply changing the target framework to .NET 4
* Log4net provides a VS2010 solution, though, I had to add a few compilation symbols (NET;NET_2_0;NET_4_0) 
  in release mode as well as removed the STRONG define. I also had it generate a pdb in release mode.
  
Both libraries are compiles in release mode.