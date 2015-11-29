#define _CRT_SECURE_NO_DEPRECATE
#include <string>
#include <sstream>
#include <Windows.h>
#include <mmsystem.h>
#include <cstdlib>
#include <cstdio>
#pragma comment(lib, "winmm.lib")
#include <iostream>
#include <stdio.h>
#include <stdlib.h>
#include <fstream>
#include <string.h>
#include <openssl/aes.h>
#include <openssl/rand.h>
#include <openssl/evp.h>


unsigned char tajneHaslo[] = "tojestsupertajnehaslo";
unsigned char test[] = "dsjfberhhbdnmdj";
// a simple hex-print routine. could be modified to print 16 bytes-per-line
void encrypt(FILE *ifp, FILE *ofp, unsigned char ckey[])
{
	//Get file size
	fseek(ifp, 0L, SEEK_END);
	int fsize = ftell(ifp);
	//set back to normal
	fseek(ifp, 0L, SEEK_SET);

	int outLen1 = 0; int outLen2 = 0;
	unsigned char *indata = (unsigned char *)malloc(fsize);
	unsigned char *outdata = (unsigned char *)malloc(fsize * 2);
	unsigned char ivec[] = "dontusethisinput";

	//Read File
	fread(indata, sizeof(char), fsize, ifp);//Read Entire File

	//Set up encryption
	EVP_CIPHER_CTX ctx;
	EVP_EncryptInit(&ctx, EVP_aes_128_cbc(), ckey, ivec);
	EVP_EncryptUpdate(&ctx, outdata, &outLen1, indata, fsize);
	EVP_EncryptFinal(&ctx, outdata + outLen1, &outLen2);
	fwrite(outdata, sizeof(char), outLen1 + outLen2, ofp);
}

void decrypt(FILE *ifp, FILE *ofp, unsigned char ckey[])
{
	//Get file size
	fseek(ifp, 0L, SEEK_END);
	int fsize = ftell(ifp);
	//set back to normal
	fseek(ifp, 0L, SEEK_SET);

	int outLen1 = 0; int outLen2 = 0;
	unsigned char *indata = (unsigned char *)malloc(fsize);
	unsigned char *outdata = (unsigned char *)malloc(fsize);
	unsigned char ivec[] = "dontusethisinput";

	//Read File
	fread(indata, sizeof(char), fsize, ifp);//Read Entire File

	//setup decryption
	EVP_CIPHER_CTX ctx;
	EVP_DecryptInit(&ctx, EVP_aes_128_cbc(), ckey, ivec);
	EVP_DecryptUpdate(&ctx, outdata, &outLen1, indata, fsize);
	EVP_DecryptFinal(&ctx, outdata + outLen1, &outLen2);
	fwrite(outdata, sizeof(char), outLen1 + outLen2, ofp);
}

inline bool exist(const std::string& name)
{
	return GetFileAttributes(name.c_str()) != INVALID_FILE_ATTRIBUTES;
}


int main(int argc, char *argv[])
{
	if (exist("konfig.txt"))
	{

		FILE *konfIN, *konfOUT;
		konfIN = fopen("konfig.txt", "rb");//File to be encrypted; plain text
		konfOUT = fopen("temp.txt", "wb");
		decrypt(konfIN, konfOUT, tajneHaslo);
		fclose(konfIN);
		fclose(konfOUT);
		std::string buffer;
		std::ifstream fin("temp.txt");
		getline(fin, buffer, char(-1));
		fin.close();
		char inpute[100];
		std::cout << "Podaj pin: " << std::endl;
		std::cin.getline(inpute, sizeof(inpute));
		std::string arr[4];
		int i = 0;
		std::stringstream ssin(buffer);
		while (ssin.good() && i < 4){
			ssin >> arr[i];
			++i;
		}
		if (remove("temp.txt") != 0)
			perror("Error deleting file");
		//std::cout << arr[0];
		if (arr[0] == inpute)
		{
			std::string buffer2;
			std::ifstream keystore("keystore.txt");
			getline(keystore, buffer2, char(-1));
			keystore.close();
			std::string arr2[4];
			i = 0;
			std::stringstream ssin(buffer2);
			while (ssin.good() && i < 4){
				ssin >> arr2[i];
				++i;
			}
			FILE *fIN, *fOUT;
			fIN = fopen("example1.mp3", "rb");//File to be written; cipher text
			fOUT = fopen("tt3.mp3", "wb");//File to be written; cipher text
			unsigned char* keyg = (unsigned char*)arr2[1].c_str();
			decrypt(fIN, fOUT, keyg);
			std::cout << arr2[1] << " . " << arr2[2] << std::endl;
			fclose(fIN);
			fclose(fOUT);
			//puszczenie mp3
			std::string ll = "tt3.mp3";
			std::string luj = "open " + ll + " type mpegvideo alias song1 ";
			MCIERROR me =
				mciSendString(luj.c_str(),
				NULL, 0, 0);

			if (me == 0)
			{
				me = mciSendString("play song1 wait", NULL, 0, 0);
				mciSendString("close song1", NULL, 0, 0);
			}
			if (remove("tt3.mp3") != 0)
				perror("Error deleting file");


		}
	}
	else
	{
		char inputf[100];
		std::cout << "Towrze konfig :" << std::endl;
		std::cout << "Podaj pin :" << std::endl;
		std::cin.getline(inputf, sizeof(inputf));
		std::string zawartosc(inputf);
		zawartosc += " keystore.txt";
		std::ofstream outd("konfigtemp.txt");
		outd << zawartosc.c_str();;
		outd.close();
		FILE *InstalfIN, *InstalfOUT;
		InstalfIN = fopen("konfigtemp.txt", "rb");//File to be encrypted; plain text
		InstalfOUT = fopen("konfig.txt", "wb");
		encrypt(InstalfIN, InstalfOUT, tajneHaslo);
		fclose(InstalfIN);
		fclose(InstalfOUT);
		if (remove("konfigtemp.txt") != 0)
			perror("Error deleting file");
	}
	FILE *fIN, *fOUT;
	fIN = fopen("example.mp3", "rb");//File to be encrypted; plain text
	fOUT = fopen("example1.mp3", "wb");//File to be written; cipher text
	//unsigned char klucz[] = "jakis tam klucz";
	encrypt(fIN, fOUT, test);
	fclose(fIN);
	fclose(fOUT);
	//Decrypt file now
	//fIN = fopen("example1.mp3", "rb");//File to be written; cipher text
	//fOUT = fopen("tt3.mp3", "wb");//File to be written; cipher text
	//decrypt(fIN, fOUT, klucz);
	//fclose(fIN);
	//fclose(fOUT);


	// free memory here

/*std::string ll = "tt3.mp3";
	std::string luj = "open " + ll + " type mpegvideo alias song1 ";
	MCIERROR me =
		mciSendString(luj.c_str(),
		NULL, 0, 0);

	if (me == 0)
	{
		me = mciSendString("play song1 wait", NULL, 0, 0);
		mciSendString("close song1", NULL, 0, 0);
	}*/
	
	return 0;
}
