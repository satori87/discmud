package com.bg.discworld.utility;

import com.mysql.cj.util.StringUtils;

public class BCConvert {  
	  
    /** 
           * The visible characters in the ASCII table start from !, and the offset value is 33 (Decimal) 
     */  
         static final char DBC_CHAR_START = 33; // half angle!  
  
    /** 
           * The characters visible in the ASCII table are ~ end, and the offset value is 126 (Decimal) 
     */  
         static final char DBC_CHAR_END = 126; // half angle~  
  
    /** 
           * Full-width corresponds to the visible characters of the ASCII table from! Start with an offset of 65281 
     */  
         static final char SBC_CHAR_START = 65281; // full angle!  
  
    /** 
           * The full angle corresponds to the visible character of the ASCII table to the end of ~, the offset value is 65374 
     */  
         static final char SBC_CHAR_END = 65374; // full angle ~  
  
    /** 
           * The relative offset of the visible characters except the spaces in the ASCII table and the corresponding full-width characters 
     */  
         static final int CONVERT_STEP = 65248; // full angle half angle conversion interval  
  
    /** 
           * The value of the full-width space, which does not follow the relative offset from ASCII and must be handled separately 
     */  
         static final char SBC_SPACE = 12288; // full-width space 12288  
  
    /** 
           * The value of a half-width space, 32 (Decimal) in ASCII 
     */  
         static final char DBC_SPACE = ' '; // half-width space  
  
    /** 
     * <PRE> 
           * Half-width characters -> full-width character conversion   
           * Only handle spaces, ! to characters between ,, ignore other 
     * </PRE> 
     */  
    public static String bj2qj(String src) {  
        if (src == null) {  
            return src;  
        }  
        StringBuilder buf = new StringBuilder(src.length());  
        char[] ca = src.toCharArray();  
        for (int i = 0; i < ca.length; i++) {  
                         if (ca[i] == DBC_SPACE) { // If it is a half-width space, replace it with a full-width space  
                buf.append(SBC_SPACE);  
                         } else if ((ca[i] >= DBC_CHAR_START) && (ca[i] <= DBC_CHAR_END)) { // The character is a visible character between !  
                buf.append((char) (ca[i] + CONVERT_STEP));  
                         } else { // Do not do anything with spaces and characters other than the visible characters in the ascii table  
                buf.append(ca[i]);  
            }  
        }  
        return buf.toString();  
    }  
  
    /** 
     * <PRE> 
           * full-width characters -> half-width character conversion   
           * Only handle full-width spaces, full-width! To the character between the full angle ~, ignore the other 
     * </PRE> 
     */  
    public static String qj2bj(String src) {  
        if (src == null) {  
            return src;  
        }  
        StringBuilder buf = new StringBuilder(src.length());  
        char[] ca = src.toCharArray();  
        for (int i = 0; i < src.length(); i++) {  
                         if (ca[i] >= SBC_CHAR_START && ca[i] <= SBC_CHAR_END) { // If at full angle! To the full angle to the interval  
                buf.append((char) (ca[i] - CONVERT_STEP));  
                         } else if (ca[i] == SBC_SPACE) { // if it is a full-width space  
                buf.append(DBC_SPACE);  
                         } else { // Do not handle full-width spaces, full-width! Characters outside the full-angle to the interval  
                buf.append(ca[i]);  
            }  
        }  
        return buf.toString();  
    }  
  
   /* public static void main(String[] args) {  
        System.out.println(StringUtils.trimToEmpty(" a,b ,c "));  
        String s = "nihaoｈｋ　｜　　　ｎｉｈｅｈｅ　，。　７８　　７　";  
        s=BCConvert.qj2bj(s);  
        System.out.println(s);  
        System.out.println(BCConvert.bj2qj(s));  
        }
   */ 
}
