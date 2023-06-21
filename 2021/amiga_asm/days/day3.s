LINES = 1000
VALUES = 12

day3:
	move.w	#3,arg_day

	bsr		day3_part1
	move.l	d0,arg_part11
	move.l	d1,arg_part12

	; bsr		day3_part2
	; move.l	d0,arg_part21
	; move.l	d1,arg_part22

	bsr		print_result

	rts

day3_part1:
    lea.l   day3_input,a0

    ; Calculate zeros and ones for each value/column
    move.w  #LINES-1,d7
.line:
    move.l  a0,a1
    lea     zeros,a2
    lea     ones,a3
    moveq   #VALUES-1,d6
.value:
    move.b  (a1)+,d0
    sub.b   #'0',d0
    tst.b   d0
    beq.s   .zero
    addq.w  #1,(a3)     ; Value is a one. Add to column in ones-collection
    bra     .next
.zero:
    addq.w  #1,(a2)     ; Value is a zero. Add to column in zeros-collection
.next:
    addq.l  #2,a2
    addq.l  #2,a3
    dbf     d6,.value
    adda.l  #VALUES,a0
    dbf     d7,.line

    ; For each value check build new binary number
    lea     zeros,a0
    lea     ones,a1
    moveq   #0,d0
    moveq   #VALUES-1,d7
.calc:
    lsl.l   #1,d0       ; Shift 1 to the left. Ex. 101 => 1010
    cmp.w   (a0)+,(a1)+ ; Is count of zeros in column less than ones?
    blo.s   .next2
    or.l    #1,d0       ; No, set bit
.next2:
    dbf     d7,.calc

    move.l  d0,d1       ; d0 = Gamma rate
    not.l   d1          ; d1 = Epsilon
    and.l   #(1<<VALUES)-1,d1

    bsr     umult64
    rts

day3_part2:
    move.w  #LINES,linecount
    moveq   #0,d5       ; Column index
    moveq   #VALUES-1,d7
.values:

    lea.l   day3_input,a0
    lea.l   indexlist,a1
    moveq   #0,d0
    moveq   #0,d1
    move.w  linecount(pc),d6
    subq.w  #1,d6
.lines:
    move.b  (a0,d5),d2
    sub.b   #'0',d2
    move.b  d2,(a1)+   
    tst.b   d2
    beq.s   .zero
    addq.w  #1,d1   ; Ones +1
    bra.s   .next
.zero:
    addq.w  #1,d0   ; Zeros +1
.next:
    adda.l  #VALUES,a0
    dbf     d6,.lines

    lea.l   day3_input,a0
    lea.l   indexlist,a1
    move.w  linecount(pc),d6
    subq.w  #1,d6
.lines2:

    cmp.w   d0,d1
    ble.s   .zero2

    bra.s   .next2
.zero2:
    ; Todo.. Create filtered list

.next2:
    dbf     d6,.lines2


    addq.w  #1,d5
    dbf     d7,.values
    rts

    even
linecount:  dc.w    0
zeros:      dcb.w   VALUES,0
ones:       dcb.w   VALUES,0
newlist:    dcb.b   LINES*VALUES,0
indexlist:  dcb.b   LINES,0

    include days/day3.dat