'use client';

import React from 'react';

import Link from 'next/link';
import Image from 'next/image';
// // Import Swiper React components
import { Swiper, SwiperSlide } from 'swiper/react';

// import required modules
import { Pagination } from 'swiper/modules';

import 'swiper/css';
import 'swiper/css/pagination';


import p1 from './../app/images/p-1.png'
import p2 from './../app/images/p-2.png'
import p3 from './../app/images/p-3.png'
import p4 from './../app/images/p-4.png'
import p5 from './../app/images/p-5.png'

import {PartnerType} from '@/type/partner'
import { getPartnerData } from '@/api/parnter';

import { useState, useEffect } from 'react';


const Partner = () => {
  const [data, setData] = useState<PartnerType[]>([]);

  const pagination = {
    clickable: true,
  };

  useEffect(() => {
    const loadData = async () => {
      // debugger;
      const result = await getPartnerData();
      setData(result);
    };

    loadData();
  }, []);

  return (
    <div className='partner'>
      <div className='wrp'>
        <div className='common-title'>\ Partner \</div>
        <Swiper pagination={pagination} slidesPerView={5}
          spaceBetween={30}
          modules={[Pagination]}
          >

          {/* {data.map(partner => (
            <SwiperSlide className='partner-item'>
              <div className='image'>
                <img src={partner.imageUrl}  style={{ width: '100%', height:'auto' }} alt='' />
              </div>
            </SwiperSlide>
          ))} */}

        </Swiper>
      </div>
    </div>

  );
};

export default Partner;