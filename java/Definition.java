
public class Definition {
	
	public Definition() {
		grammar = null;
		meaning = null;
	}
	
	public Definition(String g, String m) {
		grammar = g;
		meaning = m;
	}
	
	public String getGrammar() {
		return grammar;
	}
	
	public void setGrammar(String g) {
		grammar = g;
	}
	
	public void setMeaning(String m) {
		meaning = m;
	}
	
	public String getMeaning() {
		return meaning;
	}
	
	private String grammar;
	private String meaning;
}
