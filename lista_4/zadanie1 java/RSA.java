

import java.security.SecureRandom;
import java.io.File;
import java.math.BigInteger;
import java.util.ArrayList;
import java.util.List;

public class RSA {

    private List<BigInteger> przetestowane;
    private static SecureRandom secureRandom;
    public RSA()
    {
        this.przetestowane = new ArrayList<>();
        this.secureRandom = new SecureRandom();
    }
    public void generujKlucze(int dlugoscKlucza, int liczbaLiczbPierwszych){

        List<BigInteger> pierwszeLista = generujPierwsze(dlugoscKlucza, liczbaLiczbPierwszych);
        BigInteger n = modulRSA(pierwszeLista); //modu³ RSA
        BigInteger euler = wartoscEulera(pierwszeLista);
        BigInteger e = liczbaWzgledniePierwsza(euler);//klucz publiczny
        BigInteger d = e.modInverse(euler);//klucz prywatny

        PlikiOdczytZapis plikiOdczytZapis = new PlikiOdczytZapis(null, "klucze.txt", true);
        plikiOdczytZapis.zapis(("e: "+e.toString()+"\n ").getBytes());
        plikiOdczytZapis.zapis(("d: "+d.toString()+"\n ").getBytes());
        plikiOdczytZapis.zapis(("n: "+n.toString()+"\n ").getBytes());
        plikiOdczytZapis.zapis(("\n\n").getBytes());
        System.out.println("e: "+e+"\nd: "+d+"\nn: "+n);
    }
    public void szyfrujPlik(BigInteger e, BigInteger n, String sciezkaDoPlikuOdczyt){
        String sciezkaPlikZapis = "zaszyfrowany"+sciezkaDoPlikuOdczyt.substring(sciezkaDoPlikuOdczyt.indexOf('.'));
        PlikiOdczytZapis plikiOdczytZapis = new PlikiOdczytZapis(sciezkaDoPlikuOdczyt, sciezkaPlikZapis, false);

        byte[] zPliku = plikiOdczytZapis.odczyt((int)new File(sciezkaDoPlikuOdczyt).length());

        int liczbaZnakow = n.bitLength()/8;
        byte[] pomoc = new byte[liczbaZnakow];
        byte[] wynik;
        int j=0;

        for(int i = 0; i<zPliku.length; i++){
            if(j<liczbaZnakow){
                pomoc[j]=zPliku[i];
                j++;
            } else {
                j=0;
                i--;
                wynik = (szyfrujRSA(new BigInteger(pomoc), e, n).toString()+' ').getBytes();
                plikiOdczytZapis.zapis(wynik);
                if(zPliku.length-i>liczbaZnakow){
                    pomoc = new byte[liczbaZnakow];
                }
                else{
                    pomoc = new byte[zPliku.length%liczbaZnakow];
                }
            }
        }
        wynik = (szyfrujRSA(new BigInteger(pomoc), e, n).toString()).getBytes();
        plikiOdczytZapis.zapis(wynik);
    }

    public void deszyfrujPlik(BigInteger d, BigInteger n, String sciezkaDoPlikuOdczyt){
        String sciezkaPlikZapis = "odszyfrowany"+sciezkaDoPlikuOdczyt.substring(sciezkaDoPlikuOdczyt.indexOf('.'));
        PlikiOdczytZapis plikiOdczytZapis = new PlikiOdczytZapis(sciezkaDoPlikuOdczyt, sciezkaPlikZapis, false);

        byte[] zPliku = plikiOdczytZapis.odczyt((int)new File(sciezkaDoPlikuOdczyt).length());
        String [] odczytane = new String(zPliku).split(" ");

        for(String o: odczytane){
            BigInteger wynik= deszyfrujRSA(new BigInteger(o), d, n);
            plikiOdczytZapis.zapis(wynik.toByteArray());
        }

    }

    public BigInteger szyfrujRSA(BigInteger text, BigInteger e, BigInteger n)
    {
        return text.modPow(e, n);
    }
    public BigInteger deszyfrujRSA(BigInteger text, BigInteger d, BigInteger n){
        return text.modPow(d, n);
    }


    public List<BigInteger> generujPierwsze(int dlugoscKlucza, int liczbaKluczy){
        List<BigInteger> liczbyPierwsze = new ArrayList<>();


        for(int i=0; i<liczbaKluczy; i++){
            liczbyPierwsze.add(losujLiczbePierwsza(dlugoscKlucza));
        }

        return liczbyPierwsze;
    }
    public BigInteger modulRSA(List<BigInteger> pierwszeLista){
        BigInteger wynik=BigInteger.ONE;
        for(BigInteger b: pierwszeLista){
            wynik=wynik.multiply(b);
        }
        return wynik;
    }
    public BigInteger wartoscEulera(List<BigInteger> pierwszeLista){
        BigInteger wynik = BigInteger.ONE;
        for(BigInteger b:pierwszeLista){
            wynik=wynik.multiply(b.subtract(BigInteger.ONE));
        }
        return wynik;
    }

    public BigInteger liczbaWzgledniePierwsza(BigInteger liczba){
        int dlugosc = liczba.bitLength()-1;
        BigInteger kandydat;
        do{
            kandydat = losujLiczbePierwsza(dlugosc);
        } while(!(kandydat.gcd(liczba)).equals(BigInteger.ONE));
        return kandydat;
    }
    public BigInteger losujLiczbePierwsza(int n){

        BigInteger pierwsza;
        do {
            pierwsza =losujLiczbeNieparzysta(n);
        } while (!testPierwszosci(pierwsza));
        System.out.println("przetestowano ró¿nych liczb pierwszych: " + przetestowane.size());
        return pierwsza;
    }

    private BigInteger losujLiczbeNieparzysta(int n) {
        BigInteger pierwsza = new BigInteger(n - 2, secureRandom).add((new BigInteger("2").pow(n-2))).shiftLeft(1).add(new BigInteger("1"));
        return pierwsza;
    }

    private Boolean testPierwszosci(BigInteger liczba) {

        if(bylaTestowana(liczba)){
            return false;
        } else {
            przetestowane.add(liczba);
            return MillerRabin(liczba);
        }
    }

    private Boolean MillerRabin(BigInteger liczba) {
        BigInteger d = liczba.subtract(BigInteger.ONE);

        int s = d.getLowestSetBit();
        d = d.shiftRight(s);

        BigInteger a;
        for (int j = 0; j < 50; j++) {
            do {
                a = new BigInteger(liczba.bitLength(), secureRandom);
            } while (a.equals(BigInteger.ZERO));
            BigInteger v = a.modPow(d, liczba);
            if (v.equals(BigInteger.ONE) || v.equals(liczba.subtract(BigInteger.ONE))) {
                continue;
            }
            int i;
            for (i = 0; i < s; i++) {
                v = v.modPow(new BigInteger("2"), liczba);
                if (v.equals(liczba.subtract(BigInteger.ONE))) {
                    break;
                }
            }
            if (i == s) {
                return false;
            }
        }
        return true;
    }

    private Boolean bylaTestowana(BigInteger liczba){
        for(BigInteger l: przetestowane){
            if(l.equals(liczba)){
                return true;
            }
        }
        return false;
    }
}