using UnityEngine;
using TEngine.Localization;

public static class ScriptLocalization
{

	public static string name 		{ get{ return LocalizationManager.GetTranslation ("name"); } }
	public static string name2 		{ get{ return LocalizationManager.GetTranslation ("name2"); } }
}

public static class ScriptTerms
{

	public const string name = "name";
	public const string name2 = "name2";
}