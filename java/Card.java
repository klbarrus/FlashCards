import java.util.*;

public class Card {
	
	public Card() {
		def = new ArrayList<Definition>();
	}
	
	public String getCharacter() {
		return character;
	}
	
	public void setCharacter(String c) {
		character = c;
	}
	
	public String getPinyin() {
		return pinyin;
	}
	
	public void setPinyin(String p) {
		pinyin = p;
	}
	
	public int getNumDefinitions() {
		return def.size();
	}
	
	public Definition getDefinition(int n) {
		if ( 0 <= n && n <= def.size() - 1) {
			return def.get(n);
		}
		
		return null;
	}
	
	public void addDefinition(String grammar, String meaning) {
		Definition d = new Definition(grammar, meaning);
		def.add(d);
	}
	
	private String character;
	private String pinyin;
	private ArrayList<Definition> def;

}
