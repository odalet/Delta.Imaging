// nistwsq.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "nistwsq.h"

#include <stdlib.h>

NISTWSQ_API int __cdecl wsq_encode(
	byte** compressed_data, 
	int* compressed_data_length,
	float bitrate, 
	byte* image_data,
	int width, 
	int height, 
	int depth, 		
	int pixels_per_inch, 
	char* comment_text)
{
	return wsq_encode_mem(
		compressed_data,
		compressed_data_length, 
		bitrate, 
		image_data,	
		width, 
		height, 
		depth, 
		pixels_per_inch, 
		comment_text);
}

NISTWSQ_API int __cdecl wsq_decode(
	byte** decompressed_data,
	int* width, 
	int* height, 
	int* depth, 
	int* pixels_per_inch, 
	int* lossy_flag,
	byte* wsq_data, 
	int wsq_data_length)
{
	return wsq_decode_mem(
		decompressed_data, 
		width, 
		height, 
		depth,
		pixels_per_inch, 
		lossy_flag, 
		wsq_data, 
		wsq_data_length);
}

NISTWSQ_API int __cdecl wsq_get_comments(
		byte*** comments_data,
		int* comments_count,
		byte* wsq_data, 
		int wsq_data_length)
{
	byte* cbufptr, *ebufptr;
	cbufptr = wsq_data;
	ebufptr = wsq_data + wsq_data_length;

	int ret;
	unsigned short marker;
	byte** comments;
	int ccount = 0;

	// Get SOI
	if(ret = getc_marker_wsq(&marker, SOI_WSQ, &cbufptr, ebufptr))
		return ret;

	// Get next marker. 
	if(ret = getc_marker_wsq(&marker, ANY_WSQ, &cbufptr, ebufptr))
		return ret;

	// While not at Start of Block (SOB)
	while(marker != SOB_WSQ)
	{
		if(marker == COM_WSQ)
		{
			byte* current_comment;
			if(ret = getc_comment(&current_comment, &cbufptr, ebufptr))
				return ret;

			ccount++;
			byte** array = (byte**)malloc(sizeof(byte*) * ccount);
			if (ccount > 1)
			{
				for (int i = 0; i < ccount - 1; i++)
					array[i] = comments[i];
				free(comments);
			}

			array[ccount - 1] = current_comment;
			comments = array;
		}
		else // skip
		{
			if(ret = getc_skip_marker_segment(marker, &cbufptr, ebufptr))
				return ret;
		}

		// get next marker
		if (ret = getc_marker_wsq(&marker, ANY_WSQ, &cbufptr, ebufptr))
			return ret;
	}

	*comments_count = ccount;
	*comments_data = comments;

	return 0;
}

NISTWSQ_API void __cdecl free_mem(void* pointer)
{
	free(pointer);
}