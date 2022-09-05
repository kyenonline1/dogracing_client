#import <CoreTelephony/CTTelephonyNetworkInfo.h>
#import <CoreTelephony/CTCarrier.h>
 
char* cStringCopy(const char* string)
{
    if (string == NULL)
        return NULL;
   
    char* res = (char*)malloc(strlen(string) + 1);
    strcpy(res, string);
   
    return res;
}
 
// This takes a char* you get from Unity and converts it to an NSString* to use in your objective c code. You can mix c++ and objective c all in the same file.
static NSString* CreateNSString(const char* string)
{
    if (string != NULL)
        return [NSString stringWithUTF8String:string];
    else
        return [NSString stringWithUTF8String:""];
}
 
extern "C"
{
    bool _canOpenURL(const char* cURL)
    {
        UIApplication *application = [UIApplication sharedApplication];
        NSString *nsURL = CreateNSString(cURL);
        NSURL *URL = [NSURL URLWithString:nsURL];
        BOOL canOpen = [application canOpenURL:URL];
       
        if ( canOpen )
        {
            if ( [nsURL hasPrefix:@"tel:"] )
            {
                // Check if iOS Device supports phone calls
                CTTelephonyNetworkInfo *netInfo = [[CTTelephonyNetworkInfo alloc] init];
                CTCarrier *carrier = [netInfo subscriberCellularProvider];
                NSString *mnc = [carrier mobileNetworkCode];
                if (([mnc length] == 0) || ([mnc isEqualToString:@"65535"]))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
                return true;
        }
        return false;
    }
}