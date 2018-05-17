using System;
namespace BusinessServices
{
	public class Model
	{
		//every model has a make
		public MakeEnum Make { get; set; }
		public string Name { get; set; }
	}
	public enum MakeEnum { Ford, Crysler, Honda, Toyota }
}
