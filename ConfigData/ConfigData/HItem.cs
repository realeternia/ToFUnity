namespace ConfigDatas
{
	public class HItemConfig
	{
		public int Id;
		public string Name;
		public string Ename;
		public int SubType;
		public string Descript;
		public int Level;
		public int Rare;
		public int MaxPile;
		public int ValueFactor;
		public bool IsUsable;
		public bool IsThrowable;
		public bool ShowCollectTip;
		public int RandomGroup;
		public int Frequency;
		public string[] Attributes;
		public string Url;
		public HItemConfig(){}
		public HItemConfig(int Id,string Name,string Ename,int SubType,string Descript,int Level,int Rare,int MaxPile,int ValueFactor,bool IsUsable,bool IsThrowable,bool ShowCollectTip,int RandomGroup,int Frequency,string[] Attributes,string Url)
		{
			this.Id= Id;
			this.Name= Name;
			this.Ename= Ename;
			this.SubType= SubType;
			this.Descript= Descript;
			this.Level= Level;
			this.Rare= Rare;
			this.MaxPile= MaxPile;
			this.ValueFactor= ValueFactor;
			this.IsUsable= IsUsable;
			this.IsThrowable= IsThrowable;
			this.ShowCollectTip= ShowCollectTip;
			this.RandomGroup= RandomGroup;
			this.Frequency= Frequency;
			this.Attributes= Attributes;
			this.Url= Url;
		}
	}
}
