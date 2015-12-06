import java.io.*;

public class PlikiOdczytZapis {

    private FileOutputStream fileOutputStream;
    private FileInputStream fileInputStream;
    public PlikiOdczytZapis(String sciezkaOtworz, String sciezkaZapisz, boolean append){
        this.fileInputStream = otworzPlikOdczyt(sciezkaOtworz);
        this.fileOutputStream = otworzPlikZapis(sciezkaZapisz, append);
    }

    public synchronized byte[] odczyt(int filesize){


        byte[] zPliku = new byte[filesize];
        int calkowita=0;
        int tymczasowa=0;

        try {

            while((tymczasowa=fileInputStream.read(zPliku))!=-1){
                calkowita+=tymczasowa;
            }
            System.out.println("Odczytano "+calkowita+" bajtow");

        } catch (FileNotFoundException ex) {
            ex.printStackTrace();
        } catch (IOException ex) {
            ex.printStackTrace();
        }
        return zPliku;
    }

    public synchronized void zapis(byte[] daneDoPliku){
        try {
            fileOutputStream.write(daneDoPliku);
        } catch (FileNotFoundException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    private FileInputStream otworzPlikOdczyt(String sciezkaDoPliku){
        if(sciezkaDoPliku==null){
            return null;
        }
        File file = new File(sciezkaDoPliku);
        try {
            return new FileInputStream(file);
        } catch (FileNotFoundException e) {
            e.printStackTrace();
        }
        return null;
    }

    private FileOutputStream otworzPlikZapis(String sciezkaDoPliku, boolean append){
        File file = new File(sciezkaDoPliku);
        try {
            return new FileOutputStream(file, append);
        } catch (FileNotFoundException e) {
            e.printStackTrace();
        }
        return null;
    }
}