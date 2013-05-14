#ifdef NISTWSQ_EXPORTS
#define NISTWSQ_API __declspec(dllexport) 
#else
#define NISTWSQ_API __declspec(dllimport)
#endif

typedef unsigned char byte;

// WSQ Marker Definitions 
#define SOI_WSQ 0xffa0
#define EOI_WSQ 0xffa1
#define SOF_WSQ 0xffa2
#define SOB_WSQ 0xffa3
#define DTT_WSQ 0xffa4
#define DQT_WSQ 0xffa5
#define DHT_WSQ 0xffa6
#define DRT_WSQ 0xffa7
#define COM_WSQ 0xffa8
// Case for getting ANY marker. 
#define ANY_WSQ 0xffff

extern "C" 
{
	// internal API
	int __cdecl wsq_decode_mem(unsigned char **odata, int *ow, int *oh, int *od, int *oppi, int *lossyflag, unsigned char *idata, const int ilen);
	int __cdecl wsq_encode_mem(unsigned char **odata, int *olen, const float r_bitrate, unsigned char *idata, const int w, const int h, const int d, const int ppi, char *comment_text);
	int __cdecl getc_marker_wsq(unsigned short *, const int, unsigned char **, unsigned char *);
	int __cdecl getc_comment(unsigned char **, unsigned char **, unsigned char *);
	int __cdecl getc_skip_marker_segment(const unsigned short, unsigned char **, unsigned char *);

	// Exposed API
	NISTWSQ_API int __cdecl wsq_encode(
		byte** compressed_data, 
		int* compressed_data_length,
		float bitrate, 
		byte* image_data,
		int width, 
		int height, 
		int depth, 		
		int pixels_per_inch, 
		char* comment_text);
		
	NISTWSQ_API int __cdecl wsq_decode(
		byte** decompressed_data,
		int* width, 
		int* height, 
		int* depth, 
		int* pixels_per_inch, 
		int* lossy_flag,
		byte* wsq_data, 
		int wsq_data_length);
	
	NISTWSQ_API int __cdecl wsq_get_comments(
		byte*** comments_data,
		int* comments_count,
		byte* wsq_data, 
		int wsq_data_length);

	NISTWSQ_API void __cdecl free_mem(void* pointer);
}