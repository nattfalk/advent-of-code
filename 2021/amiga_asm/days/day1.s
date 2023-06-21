
day1:
	move.w	#1,arg_day

	bsr		day1_part1
	move.l	d0,arg_part11
	move.l	d1,arg_part12

	bsr		day1_part2
	move.l	d0,arg_part21
	move.l	d1,arg_part22

	bsr		print_result

	rts

day1_part1:
    lea.l   day1_input,a0
    move.l  #((day1_input_end-day1_input)>>1)-2,d7

    moveq   #0,d1       ; Increase count in d1
    move.w  (a0)+,d0
.loop:
    move.w  (a0)+,d2
    cmp.w   d0,d2
    bmi     .skip
    addq.w  #1,d1

.skip:
    move.w  d2,d0
    dbf     d7,.loop

    moveq   #0,d0
    rts

day1_part2:
    lea.l   day1_input,a0
    move.l  #((day1_input_end-day1_input)>>1)-4,d7

    moveq   #0,d1       ; Increase count in d1
    move.w  (a0)+,d0
    add.w   (a0),d0
    add.w   2(a0),d0
.loop2:
    move.w  (a0)+,d2
    add.w   (a0),d2
    add.w   2(a0),d2

    cmp.w   d0,d2
    ble     .skip2
    addq.w  #1,d1

.skip2:
    move.w  d2,d0
    dbf     d7,.loop2

    moveq   #0,d0
    rts

    include days/day1.dat