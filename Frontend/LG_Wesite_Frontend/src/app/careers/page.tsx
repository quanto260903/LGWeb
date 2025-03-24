'use client';

import React, { useState, useEffect } from 'react';
import Image from "next/image";
import '../css/pages/careers.scss';
import '../css/pages/_careers.scss';
import Link from 'next/link';
import bannercareer from './../../app/images/careers_banner.png'
import Recruitment from "../../components/CareersRecruitment";
import web from './../../app/images/WebcareerImage.png';
import net from './../../app/images/NetcareerImage.png';
import senior from './../../app/images/SeniorcareerImage.png';
import '../css/abstracts/_variables.scss';


import { BlogCategoryType } from '@/type/BlogCategory';
import { GetCareersTop } from '@/api/careers';


export default function Careers() {
  const [data, setData] = useState<BlogCategoryType[]>([]);

  useEffect(() => {
    const loadData = async () => {
      const result = await GetCareersTop();
      setData(result);
    };
    loadData();
  }, []);
  return (
    <>
      {/* Banner Section */}
      <div className="about-container-img" >
        <div className="banner">
          <Link href={'/'}>
            <Image
              src={bannercareer}
              alt="About Background"
              className="background-image" />
          </Link>
        </div>
        <div className="overlay-text">Careers</div>
      </div>
      <div className="job-detail-section wrp">
        <h2 className="recruitment-title tl">\ Recruitment \</h2>
        <h3 className="job-opportunities-title tl2">Job Opportunities</h3>
        {/* Other Job Opportunities */}
        <div className="other-jobs">
          <div className="job-cards wrp">
            {data && data.map((item, index) => (
              <div key={index} className="job-card">
                <div className="job-image">
                  <img
                    src={item.imageUrl || '/picture/NoImage.png'}
                    style={{ width: '100%', height: 'auto' }}
                    alt={item.name || 'Image'}
                  />
                </div>
                <div className="job-content">
                  <h4 className="tl41">{item.name}</h4>
                  <p className="tl4">{item.description}</p>
                  <a href={`/careers/detail?id=${item.id}`} className="read-more tl41">
                  Read more
                </a>
                </div>
              </div>
            ))}
          </div>
        </div>
      </div>
    </>

  );
}
