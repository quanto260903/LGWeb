'use client';

import React, { useRef, useState, useEffect } from 'react';
import Link from 'next/link';
import Image from 'next/image';

import web from './../app/images/WebcareerImage.png';
import net from './../app/images/NetcareerImage.png';
import senior from './../app/images/SeniorcareerImage.png';
import '../app/css/pages/_careers.scss';
import '../app/css/abstracts/_variables.scss';

import { BlogCategoryType } from '@/type/BlogCategory';
import { GetCareersTop } from '@/api/careers';

interface CareersProps {
  isHomePage?: boolean;
  showButton?: boolean;
  isScrollable?: boolean;
  itemLimit?: number;
}

const Careers: React.FC<CareersProps> = ({ isHomePage, showButton = true, isScrollable = true, itemLimit = 6 }) => {

  const [data, setData] = useState<BlogCategoryType[]>([]);

  useEffect(() => {
    const loadData = async () => {
      const result = await GetCareersTop();
      setData(result);
    };
    loadData();
  }, []);

  const scrollRef = useRef<HTMLDivElement | null>(null);

  const handleMouseDown = (e: React.MouseEvent<HTMLDivElement>) => {
    if (!isScrollable || !scrollRef.current) return;

    const element = scrollRef.current as HTMLDivElement & {
      isDown: boolean;
      startX: number;
    };

    element.isDown = true;
    element.startX = e.pageX - element.offsetLeft;
  };

  const handleMouseLeave = () => {
    if (!isScrollable || !scrollRef.current) return;

    const element = scrollRef.current as HTMLDivElement & { isDown: boolean };
    element.isDown = false;
  };

  const handleMouseUp = () => {
    if (!isScrollable || !scrollRef.current) return;

    const element = scrollRef.current as HTMLDivElement & { isDown: boolean };
    element.isDown = false;
  };

  const handleMouseMove = (e: React.MouseEvent<HTMLDivElement>) => {
    if (!isScrollable || !scrollRef.current) return;

    const element = scrollRef.current as HTMLDivElement & { isDown: boolean; startX: number };
    if (!element.isDown) return;

    e.preventDefault();
    const x = e.pageX - element.offsetLeft;
    const walk = (x - element.startX) * 2;
    element.scrollLeft -= walk;
  };

  return (
    <section className={`recruitment-sections ${isHomePage ? 'Job-background' : ''}`}>
      <div className="recruitment-section wrp">
        <h2 className="recruitment-title tl">\ Recruitment \</h2>
        <h3 className="job-opportunities-title tl2">Job Opportunities</h3>

        {/* Job Cards Section */}
        <div
          className={`jobs-grid ${isScrollable ? 'scrollable' : ''}`}
          ref={scrollRef}
          onMouseDown={handleMouseDown}
          onMouseLeave={handleMouseLeave}
          onMouseUp={handleMouseUp}
          onMouseMove={handleMouseMove}
        >
          {data && data.slice(0, isHomePage ? itemLimit : undefined).map((item, index) => (
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

      {showButton && (
        <div className="button-container wrp">
          <Link href={'/careers'}>
            <button className="read-more-btn ws">Read more</button>
          </Link>
        </div>
      )}
    </section>
  );
};

export default Careers;
