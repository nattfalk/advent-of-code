
day2:
	move.w	#2,arg_day

	bsr		day2_part1
	move.l	d0,arg_part11
	move.l	d1,arg_part12

	bsr		day2_part2
	move.l	d0,arg_part21
	move.l	d1,arg_part22

	bsr		print_result

	rts

day2_part1:
    lea.l   day2_input,a0
    move.l  #((day2_input_end-day2_input)>>1)-1,d7
    moveq   #0,d0       ; Horizontal position
    moveq   #0,d1       ; Depth
.loop:
    move.b  (a0)+,d2    ; Command - forward, up, down
    move.b  (a0)+,d3    ; Units
    ext.w   d3
    cmp.b   #'f',d2
    bne.s   .up
    add.w   d3,d0       ; Horizontal position += Units
    bra     .next
.up:
    cmp.b   #'u',d2
    bne.s   .down
    sub.w   d3,d1       ; Depth -= Units
    bra     .next
.down:
    cmp.b   #'d',d2
    bne.s   .next
    add.w   d3,d1       ; Depth += Units
.next:
    dbf     d7,.loop
    ext.l   d0
    ext.l   d1
    bsr     umult64     ; Result (d1:d0) = Horizontal position * Depth
    rts

day2_part2:
    lea.l   day2_input,a0
    move.l  #((day2_input_end-day2_input)>>1)-1,d7
    moveq   #0,d0       ; Horizontal position
    moveq   #0,d1       ; Depth
    move    #0,d2       ; Aim
.loop:
    move.b  (a0)+,d3    ; Command - forward, up, down
    moveq   #0,d4
    move.b  (a0)+,d4    ; Units
    cmp.b   #'f',d3
    bne.s   .up
    add.l   d4,d0       ; Horizontal position += Units
    mulu    d2,d4       ; Units *= Aim
    add.l   d4,d1       ; Depth += Aim * Units
    bra     .next
.up:
    cmp.b   #'u',d3
    bne.s   .down
    sub.l   d4,d2       ; Aim -= Units
    bra     .next
.down:
    cmp.b   #'d',d3
    bne.s   .next
    add.l   d4,d2       ; Aim += Units
.next:
    dbf     d7,.loop
    bsr     umult64     ; Result (d1:d0) = Horizontal position * Depth
    rts

    include days/day2.dat