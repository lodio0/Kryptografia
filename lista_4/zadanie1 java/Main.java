
import java.math.BigInteger;

public class Main {
    public static void main(String[] args)
    {
        RSA rsa= new RSA();
        rsa.generujKlucze(100, 3);
        System.out.println("zapisano klucze");


        BigInteger e = new BigInteger("27535779821683974469467919701058553153");
        BigInteger d = new BigInteger("12689698062779370052261845729319258817");
        BigInteger n = new BigInteger("45802681293869255473225777285933559761");
        rsa.szyfrujPlik(e,n,"test.txt");
        rsa.deszyfrujPlik(d,n,"zaszyfrowany.txt");
    }
}
