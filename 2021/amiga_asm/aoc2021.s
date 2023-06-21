	section code,code

LVOOpenLibrary		equ	-552
LVOCloseLibrary		equ	-414
LVORawDoFmt			equ	-522
LVOPutStr			equ	-948

start:
	; bsr		day1
	; bsr		day2
	bsr		day3
	rts

****************************************************
*
* Day exercises
*
****************************************************
	; include days/day1.s
	; include days/day2.s
	include days/day3.s
	even

****************************************************
*
* Helper methods
*
****************************************************
umult64:
	; Input:
	;  d0		Value 1 (32 bit)
	;  d1		Value 2 (32 bit)
	; Output:
	;  d0:d1	Result in 64 bit (high part in d0, low part in d1)
	move.l	d2,-(a7)
	move.w	d0,d2
	mulu	d1,d2
	move.l	d2,-(a7)
	move.l	d1,d2
	swap	d2
	move.w	d2,-(a7)
	mulu	d0,d2
	swap	d0
	mulu	d0,d1
	mulu	(a7)+,d0
	add.l	d2,d1
	moveq	#0,d2
	addx.w	d2,d2
	swap	d2
	swap	d1
	move.w	d1,d2
	clr.w	d1
	add.l	(a7)+,d1
	addx.l	d2,d0
	move.l	(a7)+,d2
	rts

print_result:
	movem.l	d1-d7/a0-a6,-(sp)

	move.l	4.w,a6
	move.l	a6,SysBase

	lea		dosname,a1
	moveq.l	#36,d0
	jsr		LVOOpenLibrary(a6)
	move.l	d0,DOSBase
	beq		.nodos

	lea		text,a0
	lea		text_args,a1
	lea		putchproc,a2
	lea		textbuffer,a3
	jsr		LVORawDoFmt(a6)

	move.l	a3,d1
	move.l	DOSBase,a6
	jsr		LVOPutStr(a6)

	move.l	DOSBase,a1
	move.l	SysBase,a6
	jsr		LVOCloseLibrary(a6)

.nodos:
	movem.l	(sp)+,d1-d7/a0-a6
	moveq.l	#0,d0
	rts

putchproc:
	move.b	d0,(a3)+
	rts

dosname:	dc.b	"dos.library",0

text:	dc.b	"Day %d",10
		dc.b	"- Part 1 = %ld%ld",10
		dc.b	"- Part 2 = %ld%ld",10,0

text_args:
arg_day:	dc.w	1
arg_part11:	dc.l	0
arg_part12:	dc.l	0
arg_part21:	dc.l	0
arg_part22:	dc.l	0

SysBase:	ds.l	1
DOSBase:	ds.l	1

textbuffer:	ds.b	80